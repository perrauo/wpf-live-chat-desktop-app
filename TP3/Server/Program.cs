using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IFT585_TP3.Server.Controllers;
using IFT585_TP3.Server.Model;
using IFT585_TP3.Server.Repositories;
using IFT585_TP3.Server.Repositories.MessageRepositories;
using IFT585_TP3.Server.Repositories.UserRepositories;
using IFT585_TP3.Server.RESTFramework;
using IFT585_TP3.Common.UdpServer;
using Newtonsoft.Json;
using System.Text;
using System.Net.Sockets;

namespace IFT585_TP3.Server
{    
    public class Program
    {
        private static UserInMemoryRepository userRepo = new UserInMemoryRepository();
        private static GroupInMemoryRepository groupRepo = new GroupInMemoryRepository();
        private static MessageInMemoryRepository messageRepo = new MessageInMemoryRepository();

        private static UserController userCtrl;
        private static GroupController groupCtrl;
        private static MessageController messageCtrl;

        static void Main(string[] args)
        {
            // Add default admin
            var adminSalt = PasswordHelper.GenerateSalt();
            userRepo.Create(new User()
            {
                Username = "admin",
                PasswordSalt = adminSalt,
                PasswordHash = PasswordHelper.Hash("admin", adminSalt)
            });
            
            var socket = new UDPSocket();
            socket.Server("127.0.0.1", 24000);
            socket.MessageReceived += Socket_MessageReceived;

            Console.WriteLine("Server is running, ctrl+c to terminate.");
            /////////////////////////////////////////////////////////////////////////////////////////////////
            /// /!\ This line must be executed last, it contains an infinite loop running the server. /!\ ///
            /////////////////////////////////////////////////////////////////////////////////////////////////
            InitWebServer();
        }

        private static void Socket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var socket = (UDPSocket)sender;

            var creds = JsonConvert.DeserializeObject<Credential>(e.Message);
            if (ValidateCredentials(creds))
            {
                socket.Respond(e.EndPoint, JWTHelper.Generate(creds.userName));
            }
            else if (!userRepo.Exists(creds.userName))
            {
                userRepo.Create(new User(creds));
                socket.Respond(e.EndPoint, JWTHelper.Generate(creds.userName));
            }
            else
            {
                socket.Respond(e.EndPoint, "LOGIN_ERROR");
            }
        }

        public static bool ValidateCredentials(Credential credentials)
        {
            var user = userRepo.Retrieve(credentials.userName);
            if (user != null && PasswordHelper.Hash(credentials.password, user.PasswordSalt) == user.PasswordHash)
            {
                return true;
            }
            return false;
        }

        static void InitWebServer()
        {
            // init controllers and repositories
            userCtrl = new UserController() { UserRepo = userRepo };
            groupCtrl = new GroupController() { UserRepo = userRepo, GroupRepo = groupRepo };
            messageCtrl = new MessageController() { GroupRepo = groupRepo, MessageRepo = messageRepo };

            var chatApi = new RESTFramework.Server();

            // register route handlers
            chatApi.Use(AttachUser);
            userCtrl.RegisterRoutes(chatApi);
            groupCtrl.RegisterRoutes(chatApi);
            messageCtrl.RegisterRoutes(chatApi);

            chatApi.Listen("http://localhost:8090/"); // TODO: Change to port 80 and figure out how to run it without admin rights.
        }

        static async Task AttachUser(Request req, Response res)
        {
            var accessToken = req.Headers.Get("x-access-token");
            if (!JWTHelper.Verify(accessToken))
            {
                await res.Unauthorized("Cannot authenticate the user. The access token is not valid.");
                return;
            }

            var username = JWTHelper.ExtractUserFromToken(accessToken);
            var authUser = userRepo.Retrieve(username);
            authUser.LastActivity = DateTime.Now;
            userRepo.Update(authUser);

            req.Context.AuthenticatedUser = authUser;

            Console.WriteLine(req);
        }
    }
}

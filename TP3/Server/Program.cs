using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IFT585_TP3.Common;
using IFT585_TP3.Server.Controllers;
using IFT585_TP3.Server.Model;
using IFT585_TP3.Server.Repositories;
using IFT585_TP3.Server.Repositories.MessageRepositories;
using IFT585_TP3.Server.Repositories.UserRepositories;
using IFT585_TP3.Server.RESTFramework;

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

            // TODO: Run the UDP server here !

            Console.WriteLine("Server is running, ctrl+c to terminate.");
            /////////////////////////////////////////////////////////////////////////////////////////////////
            /// /!\ This line must be executed last, it contains an infinite loop running the server. /!\ ///
            /////////////////////////////////////////////////////////////////////////////////////////////////
            InitWebServer();
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

            chatApi.Listen("http://localhost:8090/");
        }

        static async Task AttachUser(Request req, Response res)
        {
            req.Context.AuthenticatedUser = userRepo.Retrieve("admin");
            req.Context.AuthenticatedUser.LastActivity = DateTime.Now;
            userRepo.Update(req.Context.AuthenticatedUser);

            /* TODO: uncomment when UDP auth is working !
            var accessToken = req.Headers.Get("x-access-token");
            if (!JWTHelper.Verify(accessToken))
            {
                await res.Unauthorized("Cannot authenticate the user. The access token is not valid.");
                return;
            }

            var username = JWTHelper.ExtractUserFromToken(accessToken);

            req.Context.AuthenticatedUser = userRepo.Retrieve(username);
            */
        }
    }
}

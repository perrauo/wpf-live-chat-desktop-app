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
        public static Dictionary<string, byte[]> _users = new Dictionary<string, byte[]>();
        private static UserInMemoryRepository userRepo = new UserInMemoryRepository();
        private static GroupInMemoryRepository groupRepo = new GroupInMemoryRepository();
        private static MessageInMemoryRepository messageRepo = new MessageInMemoryRepository();

        private static UserController userCtrl;
        private static GroupController groupCtrl;

        static void Main(string[] args)
        {
            // Add default admin
            _users.Add("admin", Utils.HashPasswordWithSalt("admin".ToASCII(), Utils.GenerateSalt()));

            // init controllers and repositories
            userCtrl = new UserController() { UserRepo = userRepo };
            groupCtrl = new GroupController() { UserRepo = userRepo, GroupRepo = groupRepo, MessageRepo = messageRepo };

            var chatApi = new RESTFramework.Server();

            // register route handlers
            chatApi.Use(AttachUser);
            userCtrl.RegisterRoutes(chatApi);
            groupCtrl.RegisterRoutes(chatApi);

            Console.WriteLine("Server is running, ctrl+c to terminate.");
            chatApi.Listen("http://localhost:8090/");
        }

        static async Task AttachUser(Request req, Response res)
        {
            // TODO: utiliser le token que le client place dans les headers pour déterminer si le user est bien auth et le placer dans le contexte.
            req.Context.AuthenticatedUser = new User();
        }
    }
}

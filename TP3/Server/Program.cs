using System;
using System.Collections.Generic;
using IFT585_TP3.Common;
using IFT585_TP3.Server.Controllers;

namespace IFT585_TP3.Server
{    
    public class Program
    {
        public static Dictionary<string, byte[]> _users = new Dictionary<string, byte[]>();

        static void Main(string[] args)
        {
            // Add default admin
            _users.Add("admin", Utils.HashPasswordWithSalt("admin".ToASCII(), Utils.GenerateSalt()));


            var chatApi = new RESTFramework.Server();
            // TODO: Add security middleware with a new syntax: chatApi.Use(MiddlewareFunction) to apply it to
            //       all routes. The security thing will add a root context with the user model in it.

            UserController.RegisterRoutes(chatApi);
            GroupController.RegisterRoutes(chatApi);

            Console.WriteLine("Server is running, ctrl+c to terminate.");
            chatApi.Listen("http://localhost:8080/");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFT585_TP3.Common;
using IFT585_TP3.Server.Framework;

namespace IFT585_TP3.Server
{    
    public class Program
    {
        private static int value = 42;

        public static Dictionary<string, byte[]> _users = new Dictionary<string, byte[]>();

        static void Main(string[] args)
        {
            // Add default admin
            _users.Add("admin", Utils.HashPasswordWithSalt("admin".ToASCII(), Utils.GenerateSalt()));

            var exampleServer = new Framework.Server();
            exampleServer.Use(Method.GET, "/api/universe", GetAnswerToTheUniverse);
            exampleServer.Listen("http://localhost:8080/");
        }

        private static async Task GetAnswerToTheUniverse(Request req, Response res)
        {
            await res.Json(new ExampleResponse { Value = value });
        }

        private class ExampleResponse
        {
            public int Value { get; set; }
        }
    }
}

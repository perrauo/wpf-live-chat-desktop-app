using IFT585_TP3.Server.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.Controller
{
    static public class UserController
    {
        public static void RegisterRoutes(Framework.Server server)
        {
            server.Use(Method.GET, "/api/user", GetUsers);
            server.Use(Method.DELETE, "/api/user/:username", DeleteUser);
        }

        static async Task GetUsers(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static async Task DeleteUser(Request req, Response res)
        {
            var username = req.Params.Get("username");
            await res.InternalError("Un-implemented route.");
        }
    }
}

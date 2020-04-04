using IFT585_TP3.Common.Reponses;
using IFT585_TP3.Server.Repositories.UserRepositories;
using IFT585_TP3.Server.RESTFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.Controllers
{
    public class UserController
    {
        public UserInMemoryRepository UserRepo { get; set; }

        public void RegisterRoutes(RESTFramework.Server server)
        {
            server.Use(Method.GET, "/api/user", GetUsers);
            server.Use(Method.DELETE, "/api/user/:username", DeleteUser);
        }

        /// <summary>
        /// Return the list of all users.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        private async Task GetUsers(Request req, Response res)
        {
            var users = UserRepo.RetrieveAll();

            await res.Json(new UserListResponse() { 
                Users = users.Select(_user => new Common.Reponses.User()
                {
                     Username = _user.Username,
                     LastActivity = _user.LastActivity
                })
            });
        }

        private async Task DeleteUser(Request req, Response res)
        {
            var username = req.Params.Get("username");
            
            if (req.Context.AuthenticatedUser.Username != "admin")
            {
                await res.Unauthorized("Action requires admin permission.");
            }

            UserRepo.Delete(username);

            res.Close();
        }
    }
}

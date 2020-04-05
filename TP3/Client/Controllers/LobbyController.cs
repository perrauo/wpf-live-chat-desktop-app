using IFT585_TP3.Client.Controllers;
using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Common.Reponses;
using IFT585_TP3.Common.UdpServer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Controllers
{
    public class LobbyController : Controller
    {
        public LobbyController(Connection connection) : base(connection) { }

        public async Task<Result<GroupListResponse>> GetGroups()
        {
            return await Get<GroupListResponse>("/api/group");
        }

        public async Task<Result> CreateGroup(Group group)
        {
            var content = NetworkHelper.WrapContent<Group>(group);
            return await Post("/api/group", content);
        }

        public async Task<Result> CreateUser(string username)
        {
            var content = NetworkHelper.WrapContent<Credential>(new Credential()
            {
                userName = username,
                password = "a" // TODO: maybe fix that
            });
            return await Post("/api/user", content);
        }

        public async Task<Result> DeleteUser(string username)
        {
            return await Delete($"/api/user/{username}");
        }

    }
}

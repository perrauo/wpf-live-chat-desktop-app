using IFT585_TP3.Server.Controller.Context;
using IFT585_TP3.Server.Framework;
using System.Threading.Tasks;
using IFT585_TP3.Common.Model;

namespace IFT585_TP3.Server.Controller
{
    static public class GroupController
    {
        static public void RegisterRoutes(Framework.Server server)
        {
            server.Use(Method.GET, "/api/group", GetAllForUser);
            server.Use(Method.POST, "/api/group", CreateGroup);

            server.Use(Method.GET, "/api/group/:group_name", GroupNameMiddleware, GetOne);
            server.Use(Method.DELETE, "/api/group/:group_name", GroupNameMiddleware, Delete);

            server.Use(Method.POST, "/api/group/:group_name/invitation", GroupNameMiddleware, Invite);
            server.Use(Method.PUT, "/api/group/:group_name/invitation", GroupNameMiddleware, AcceptInvitation);
            server.Use(Method.DELETE, "/api/group/:group_name/invitation", GroupNameMiddleware, RefuseInvitation);

            server.Use(Method.GET, "/api/group/:group_name/message", GroupNameMiddleware, GetMessages);
            server.Use(Method.POST, "/api/group/:group_name/message", GroupNameMiddleware, AddMessage);

            server.Use(Method.POST, "/api/group/:group_name/admin", GroupNameMiddleware, SetGroupAdmin);
            server.Use(Method.DELETE, "/api/group/:group_name/admin", GroupNameMiddleware, RevokeGroupAdmin);
        }

        static private async Task GetAllForUser(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task CreateGroup(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task GroupNameMiddleware(Request req, Response res)
        {
            var groupName = req.Params.Get("group_name");
            Group group = null;

            if (group == null)
            {
                await res.BadRequest($"No group with name {groupName}.");
            } 
            else
            {
                req.Context = new GroupContext { Group = group };
            }
        }

        static private async Task GetOne(Request req, Response res)
        {
            var group = ((GroupContext)req.Context).Group;
            await res.InternalError("Un-implemented route.");
        }

        static private async Task Delete(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task Invite(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task AcceptInvitation(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task RefuseInvitation(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task GetMessages(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task AddMessage(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task SetGroupAdmin(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }

        static private async Task RevokeGroupAdmin(Request req, Response res)
        {
            await res.InternalError("Un-implemented route.");
        }
    }
}


using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Common.Reponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Controllers
{
    public class GroupChatController : Controller
    {
        public GroupChatController(Connection connection) : base(connection) { }

        public async Task<Result<Group>> GetGroup(string groupName)
        {
            return await Get<Group>($"/api/group/{groupName}");
        }

        public async Task<Result<MessageListReponse>> GetMessages(string groupName, DateTime lastUpdate)
        {
            return await Get<MessageListReponse>($"/api/message/{groupName}?from={lastUpdate.ToString("o")}");
        }

        public async Task<Result> SendMessage(Message message)
        {
            var content = NetworkHelper.WrapContent<Message>(message);
            return await Post($"/api/message/{message.GroupName}", content);
        }

        public async Task<Result> RemoveUser(string groupName, string username)
        {
            return await Delete($"/api/group/{groupName}/{username}");
        }

        public async Task<Result> MakeAdmin(string groupName, string username)
        {
            return await Post($"/api/group/{groupName}/admin/{username}");
        }

        public async Task<Result> InviteUser(string groupName, string username)
        {
            return await Post($"/api/group/{groupName}/invitation/{username}");
        }
    }
}

using IFT585_TP3.Client.Model;
using IFT585_TP3.Client.Repositories.GroupRepositories;
using IFT585_TP3.Client.Repositories.MessageRepositories;
using IFT585_TP3.Common;
using System.Collections.Generic;

namespace IFT585_TP3.Client
{
    public class GroupChatController
    {
        private GroupWebRepository _groupRepository;
        private MessageWebRepository _messageRepository;

        public Result<object> SendGroupInvite(string groupname)
        {
            return new Result<object>();
        }

        public void SendAdminRequest(string username) {
        }

        public void DeclineAdminRequest(string username)
        {

        }

        // Moved from LobbyController
        // TODO: Change UML
        public IEnumerable<User> GetConnectUsers(string groupname)
        {
            return null;
        }
    }
}

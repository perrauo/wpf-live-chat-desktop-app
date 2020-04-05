
using IFT585_TP3.Common;
using IFT585_TP3.Common.Reponses;
using System.Collections.Generic;

namespace IFT585_TP3.Client
{
    public class GroupChatController
    {
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

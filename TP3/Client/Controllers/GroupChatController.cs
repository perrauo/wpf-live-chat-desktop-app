using IFT585_TP3.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client
{
    public class GroupChatController
    {
        private Network.GroupWebRepository _groupRepository = new Network.GroupWebRepository();
        private Network.MessageWebRepository _messageRepository = new Network.MessageWebRepository();

        public Common.Result<object> SendGroupInvite(string groupname)
        {
            return new Common.Result<object>();
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

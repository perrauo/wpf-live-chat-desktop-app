using IFT585_TP3.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client
{
    public class LobbyController
    {
        private Network.GroupWebRepository _groupRepository = new Network.GroupWebRepository();
        private Network.UserWebRepository _userRepository = new Network.UserWebRepository();

        // TODO
        public bool GroupExists(string groupname)
        {
            return false;
        }

        // TODO
        public Common.Result<Group> GetGroup(string groupname)
        {
            return new Common.Result<Group>();
        }


    }
}

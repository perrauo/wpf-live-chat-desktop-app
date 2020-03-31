using IFT585_TP3.Client.Model;
using IFT585_TP3.Client.Repositories.GroupRepositories;
using IFT585_TP3.Client.Repositories.UserRepositories;
using IFT585_TP3.Common;

namespace IFT585_TP3.Client
{
    public class LobbyController
    {
        private IGroupRepository _groupRepository;
        private IUserRepository _userRepository;

        // TODO
        public bool GroupExists(string groupname)
        {
            return false;
        }

        // TODO
        public Result<Group> GetGroup(string groupname)
        {
            return new Result<Group>();
        }


    }
}

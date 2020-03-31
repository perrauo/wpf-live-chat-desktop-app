using IFT585_TP3.Common;
using IFT585_TP3.Server.Repositories;
using System.Collections.Generic;

namespace IFT585_TP3.Server.Model
{
    public class Group : IDeepClonable<Group>
    {
        [Id]
        public string GroupName { get; set; } = "";

        public int NumActiveMembers => ConnectedUsernames.Count;

        public int NumMembers => MemberUsernames.Count;

        public List<string> ConnectedUsernames { get; set; } = new List<string>();

        public List<string> MemberUsernames { get; set; } = new List<string>();

        public List<string> InvitedUsernames { get; set; } = new List<string>();

        public List<string> PendingAdminUsernames { get; set; } = new List<string>();

        public List<string> AdminUsernames { get; set; } = new List<string>();

        public Group DeepClone()
        {
            Group clone = new Group
            {
                GroupName = GroupName,
                ConnectedUsernames = ConnectedUsernames.ConvertAll(connectedUsername => connectedUsername),
                MemberUsernames = MemberUsernames.ConvertAll(memberUsername => memberUsername),
                InvitedUsernames = InvitedUsernames.ConvertAll(invitedUsername => invitedUsername),
                PendingAdminUsernames = PendingAdminUsernames.ConvertAll(pendingUsername => pendingUsername),
                AdminUsernames = AdminUsernames.ConvertAll(adminUsername => adminUsername)
            };

            return clone;
        }
    }
}

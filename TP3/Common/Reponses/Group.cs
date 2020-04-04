using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Common.Reponses
{
    public class Group
    {
        public string GroupName { get; set; } = "";

        public int NumActiveMembers => ConnectedUsernames.Count;

        public int NumMembers => MemberUsernames.Count;

        public List<string> ConnectedUsernames { get; set; } = new List<string>();

        public List<string> MemberUsernames { get; set; } = new List<string>();

        public List<string> InvitedUsernames { get; set; } = new List<string>();

        public List<string> PendingAdminUsernames { get; set; } = new List<string>();

        public List<string> AdminUsernames { get; set; } = new List<string>();
    }
}

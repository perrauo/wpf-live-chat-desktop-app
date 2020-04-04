using IFT585_TP3.Server.Model;
using IFT585_TP3.Server.RESTFramework;

namespace IFT585_TP3.Server.Controllers.Context
{
    public class GroupContext : RootContext
    {
        public Group Group { get; set; }
    }
}

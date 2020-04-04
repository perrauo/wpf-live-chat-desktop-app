using IFT585_TP3.Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.RESTFramework
{
    public class RootContext
    {
        public User AuthenticatedUser { get; set; }
    }
}

using IFT585_TP3.Common.Model;
using IFT585_TP3.Server.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Server.Controller.Context
{
    public class GroupContext : IContext
    {
        public Group Group { get; set; }
    }
}

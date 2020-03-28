using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Network
{
    public class Connection
    {
        public bool IsAdmin { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
    }
}

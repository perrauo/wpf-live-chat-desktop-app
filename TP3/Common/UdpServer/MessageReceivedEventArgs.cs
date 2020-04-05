using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Common.UdpServer
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Message { get; set; }
        public EndPoint EndPoint { get; set; }
    }
}

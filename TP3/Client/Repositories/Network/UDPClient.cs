using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client.Network
{
    public class UDPClient
    {
        public Common.Result<Network.Connection> Connect(string username, byte[] password)
        {
            // TODO do actual connection
            return new Common.Result<Network.Connection>
            {
                Return = new Network.Connection
                {
                    IsAdmin = true,
                    Username = username,
                    Password = password

                },
                Status = Common.Status.Success
            };
        }
    }
}

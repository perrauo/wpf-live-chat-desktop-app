using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IFT585_TP3.Common;

namespace IFT585_TP3.Client
{
    public class ConnectionController
    {
        private Network.UDPClient _udpClient = new Network.UDPClient();

        public Result<Network.Connection> Connect(string username, string password)
        {
            if (!Utils.IsValidUserName(username)) return new Result<Network.Connection> { Status = Status.Login_InvalidUsernameError };
            if (!Utils.IsValidUserName(password)) return new Result<Network.Connection> { Status = Status.Login_InvalidPasswordError };

            // TODO do UDP connection
            return _udpClient.Connect(
                username, 
                Common.Utils.HashPasswordWithSalt(password.ToASCII(), Common.Utils.GenerateSalt()));
        }
    }
}

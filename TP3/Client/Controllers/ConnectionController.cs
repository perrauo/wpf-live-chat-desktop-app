using IFT585_TP3.Common;

namespace IFT585_TP3.Client
{
    public class ConnectionController
    {
        private NetworkFramework.UDPClient _udpClient = new NetworkFramework.UDPClient();

        public Result<NetworkFramework.Connection> Connect(string username, string password)
        {
            if (!Utils.IsValidUserName(username)) return new Result<NetworkFramework.Connection> { Status = Status.Login_InvalidUsernameError };
            if (!Utils.IsValidUserName(password)) return new Result<NetworkFramework.Connection> { Status = Status.Login_InvalidPasswordError };

            // TODO do UDP connection
            return _udpClient.Connect(
                username, 
                Common.Utils.HashPasswordWithSalt(password.ToASCII(), Common.Utils.GenerateSalt()));
        }
    }
}

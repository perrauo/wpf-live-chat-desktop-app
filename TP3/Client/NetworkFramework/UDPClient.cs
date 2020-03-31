using IFT585_TP3.Common;

namespace IFT585_TP3.Client.NetworkFramework
{
    public class UDPClient
    {
        public Result<Connection> Connect(string username, byte[] password)
        {
            // TODO do actual connection
            return new Common.Result<Connection>
            {
                Return = new Connection
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

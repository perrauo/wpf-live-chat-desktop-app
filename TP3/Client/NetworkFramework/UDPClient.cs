using IFT585_TP3.Common;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IFT585_TP3.Client.NetworkFramework
{
    public class UDPClient
    {
        Socket m_SockBroadcastSender;
        IPEndPoint m_IPEPBroadcast;
        IPEndPoint m_IPEPLocal;
        private EndPoint mChatServerEp;

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

        public UDPClient()
        {

        }

        public UDPClient(int LocalPort, int remotePort)
        {
            m_IPEPBroadcast = new IPEndPoint(IPAddress.Broadcast, remotePort);
            m_IPEPLocal = new IPEndPoint(IPAddress.Any, LocalPort);

            m_SockBroadcastSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            m_SockBroadcastSender.EnableBroadcast = true;
        }

        public void sendBroadcast(string dataSender)
        {
            if (string.IsNullOrEmpty(dataSender))
            {
                return;
            }
            try
            {
                if (!m_SockBroadcastSender.IsBound)
                {
                    m_SockBroadcastSender.Bind(m_IPEPLocal);
                }

                var dataBytes = Encoding.ASCII.GetBytes(dataSender);
                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(dataBytes, 0, dataBytes.Length);
                saea.RemoteEndPoint = m_IPEPBroadcast;

                saea.Completed += sendCompletedCallBack;

                m_SockBroadcastSender.SendToAsync(saea);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                throw;
            }
        }

        private void sendCompletedCallBack(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine($"Data sent sucessfully to {e.RemoteEndPoint}");

            if (Encoding.ASCII.GetString(e.Buffer).Equals("<DISCOVER>"))
            {
                ReceivedTextFromServer(expedtedValue: "<CONFIRM>", IPEPReceiverLocal: m_IPEPLocal);
            }

        }

        private void ReceivedTextFromServer(string expedtedValue, IPEndPoint IPEPReceiverLocal)
        {
            if (IPEPReceiverLocal == null)
            {
                Console.WriteLine("No IPEndPoint specified");
                return;
            }

            SocketAsyncEventArgs saeaSendConfirmation = new SocketAsyncEventArgs();

            saeaSendConfirmation.SetBuffer(new byte[1024], 0, 1024);
            saeaSendConfirmation.RemoteEndPoint = IPEPReceiverLocal;

            saeaSendConfirmation.UserToken = expedtedValue;

            saeaSendConfirmation.Completed += ReceiveConfirmationCompleted;

            m_SockBroadcastSender.ReceiveFromAsync(saeaSendConfirmation);
        }

        private void ReceiveConfirmationCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred == 0)
            {
                Debug.WriteLine($"Zero bytes transferred, socket error: {e.SocketError}");
            }

            var receivedText = Encoding.ASCII.GetString(e.Buffer, 0, e.BytesTransferred);

            if (receivedText.Equals(Convert.ToString(e.UserToken)))
            {
                Console.WriteLine($"Received confirmation from server. {e.RemoteEndPoint}");
                mChatServerEp = e.RemoteEndPoint;
                ReceivedTextFromServer(string.Empty, mChatServerEp as IPEndPoint);
            }
            else
            {
                Console.WriteLine("Expected text not received.");
            }
        }
    }
}


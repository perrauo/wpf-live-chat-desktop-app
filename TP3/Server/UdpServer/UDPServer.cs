using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IFT585_TP3.Server.UdpServer
{
    class UDPServer
    {

        Socket m_SocketBroadcastReceiver;
        IPEndPoint m_IpEndLocal;
        private int retryCount = 0;

        List<EndPoint> m_Listclient;
        public UDPServer()
        {
            m_SocketBroadcastReceiver = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_IpEndLocal = new IPEndPoint(IPAddress.Any, 23000);

            m_SocketBroadcastReceiver.EnableBroadcast = true;

            m_Listclient = new List<EndPoint>();
        }


        public void StartReceivingData()
        {
            try
            {
                SocketAsyncEventArgs saea = new SocketAsyncEventArgs();
                saea.SetBuffer(new byte[1024], 0, 1024);
                saea.RemoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                if (!m_SocketBroadcastReceiver.IsBound)
                {
                    m_SocketBroadcastReceiver.Bind(m_IpEndLocal);
                }

                saea.Completed += ReceivedCompletedCallback;

                if (!m_SocketBroadcastReceiver.ReceiveFromAsync(saea))
                {
                    Console.WriteLine($"Failed to receive data - socket error: {saea.SocketError} ");
                    if (retryCount++ >= 10)
                    {
                        return;
                    }
                    else
                    {
                        StartReceivingData();
                    }
                }
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
                throw;
            }
        }

        private void ReceivedCompletedCallback(object sender, SocketAsyncEventArgs e)
        {
            string textReceived = Encoding.ASCII.GetString(e.Buffer, 0, e.BytesTransferred);
            Console.WriteLine($" Le texte suivant : {textReceived} a bel et bien ete recus {Environment.NewLine}" +
                $"Le nombre de Bytes recu est de: {e.BytesTransferred}" +
                $"Donne recu du point final: {e.RemoteEndPoint}{Environment.NewLine}");
            if (textReceived.Equals("<DISCOVER>"))
            {
                if (!m_Listclient.Contains(e.RemoteEndPoint))
                {
                    m_Listclient.Add(e.RemoteEndPoint);
                    Console.WriteLine("Total Clients " + m_Listclient.Count);
                }

                sendTextEndPoint("<CONFIRMATION", e.RemoteEndPoint);
            }
            StartReceivingData();
        }

        private void sendTextEndPoint(string textToSend, EndPoint remoteEndPoint)
        {
            if (string.IsNullOrEmpty(textToSend) || remoteEndPoint == null)
            {
                return;
            }
            SocketAsyncEventArgs saeaSend = new SocketAsyncEventArgs();
            saeaSend.RemoteEndPoint = remoteEndPoint;

            var bytesToSend = Encoding.ASCII.GetBytes(textToSend);

            saeaSend.SetBuffer(bytesToSend, 0, bytesToSend.Length);

            saeaSend.Completed += sendTextToEndPointCompleted;

            m_SocketBroadcastReceiver.SendToAsync(saeaSend);
        }

        private void sendTextToEndPointCompleted(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine($"Completed sending text to {e.RemoteEndPoint}");
        }

        public class UDP_ACHAT
        {
        }
    }
}


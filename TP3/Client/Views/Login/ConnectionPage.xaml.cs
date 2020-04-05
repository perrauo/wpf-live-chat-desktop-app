using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Common.UdpServer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net.Sockets;

namespace IFT585_TP3.Client
{
    /// <summary>
    /// Interaction logic for ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : BasePage
    {
        private string portInput = "PortInput";
        private string remotePortInput = "RemotePortInput";

        private const string LoginUsernameInputString = "LoginUsernameInput";
        private TextBox _loginUsernameInput;

        private const string LoginPasswordInputString = "LoginPasswordInput";
        private TextBox _loginPasswordInput;

        private const string RegisterUsernameInputString = "LoginUsernameInput";
        private TextBox _registerUsernameInput;

        public Action<Connection> OnConnectedHandler { get; set; }

        private UDPSocket _socket = new UDPSocket();



        public ConnectionPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;

            _socket.MessageReceived += _socket_MessageReceived;
            _socket.Client("127.0.0.1", 24000);
            
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            List<TextBox> results = new List<TextBox>();
            Utils.FindChildren(results, this);

            _loginUsernameInput = results.Find(item => item.Name.Equals(LoginUsernameInputString));
            _loginPasswordInput = results.Find(item => item.Name.Equals(LoginPasswordInputString));
        }

        //When login button is pressed, it tries to login user
        private void OnLoginButtonClicked(object sender, RoutedEventArgs e)
        {
            var credentials = new Credential();
            
            credentials.userName = _loginUsernameInput.Text;
            credentials.password = _loginPasswordInput.Text;
            var json = JsonConvert.SerializeObject(credentials);

            _socket.Send(json);
        }

        private void _socket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            // wierd nesting in a lambda expression to bind the 'this' context to the UI thread.
            this.Dispatcher.Invoke(() =>
            {
                if (e.Message == "LOGIN_ERROR")
                {
                    NotificationService.OnNotificationStaticHandler?.Invoke(NotificationType.Error, "No match found for this username and password.");
                }
                else
                {
                    OnConnectedHandler?.Invoke(new Connection
                    {
                        Username = _loginUsernameInput.Text,
                        AccessToken = e.Message
                    });
                }
            });
        }

    }
}

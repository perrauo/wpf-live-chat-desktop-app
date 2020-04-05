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


namespace IFT585_TP3.Client
{
    /// <summary>
    /// Interaction logic for ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : BasePage
    {
        private ConnectionController _connectionController = new ConnectionController();

        private string portInput = "PortInput";
        private string remotePortInput = "RemotePortInput";

        private const string LoginUsernameInputString = "LoginUsernameInput";
        private TextBox _loginUsernameInput;

        private const string LoginPasswordInputString = "LoginPasswordInput";
        private TextBox _loginPasswordInput;


        private const string RegisterUsernameInputString = "LoginUsernameInput";
        private TextBox _registerUsernameInput;

        public Action<Connection> OnConnectedHandler { get; set; }

       
        
        private UDPClient mChatClient;
        credential cossin2;





        public ConnectionPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            
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
            var credentials = new credential();
            if(mChatClient == null)
            {
               
                mChatClient = new UDPClient(23001, 23000);  
            }
            credentials.userName = _loginUsernameInput.Text;
            credentials.password = _loginPasswordInput.Text;
            var cossin = JsonConvert.SerializeObject(credentials);
            mChatClient.sendBroadcast(cossin);

            cossin2 = JsonConvert.DeserializeObject<credential>(cossin);

            




        }

        public credential GetCredential()
        {
            return cossin2; 
        }

        


       
    }
}

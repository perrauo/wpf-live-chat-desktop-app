using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IFT585_TP3.Client
{
    /// <summary>
    /// Interaction logic for ConnectionPage.xaml
    /// </summary>
    public partial class ConnectionPage : BasePage
    {
        private ConnectionController _connectionController = new ConnectionController();

        private const string LoginUsernameInputString = "LoginUsernameInput";
        private TextBox _loginUsernameInput;

        private const string LoginPasswordInputString = "LoginPasswordInput";
        private TextBox _loginPasswordInput;


        private const string RegisterUsernameInputString = "LoginUsernameInput";
        private TextBox _registerUsernameInput;

        public Action<Network.Connection> OnConnectedHandler { get; set; }
    
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
            Common.Result<Network.Connection> res = _connectionController.Connect(_loginUsernameInput.Text, _loginPasswordInput.Text);
            switch (res.Status)
            {
                case Common.Status.Login_InvalidPasswordError:
                case Common.Status.Login_InvalidUsernameError:
                case Common.Status.Login_AlreadyExistUsernameError:
                    //TODO
                    NotificationService.OnNotificationStaticHandler?.Invoke(NotificationType.Error, NotificationService.UnknownErrorMessage);
                    return;
                case Common.Status.Success:
                    NotificationService.OnNotificationStaticHandler?.Invoke(NotificationType.Success, NotificationService.LoginSuccessMessage);
                    OnConnectedHandler?.Invoke(res.Return);
                    break;
            }
        }

    }
}

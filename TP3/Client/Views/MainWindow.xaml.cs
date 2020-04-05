using IFT585_TP3.Client.NetworkFramework;
using IFT585_TP3.Common.Reponses;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string RootGridString = "Root";

        private ConnectionPage _connectionPage;
        private LobbyPage _lobbyPage;
        private GroupChatPage _groupChatPage;
        private BaseToastControl _toastControl;
        private Connection _connection;
        private Grid _rootGrid;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnMainWindowLoaded;
        }

        public void OnNotification(NotificationType type, string message)
        {
            BaseToastControl toast = null;
            switch (type)
            {
                case NotificationType.Error:
                    toast = new ErrorToastControl()
                    {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        RenderTransform = new TranslateTransform(),
                    };
                    break;
                case NotificationType.Success:
                    toast = new SuccessToastControl()
                    {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        RenderTransform = new TranslateTransform(),
                        Text = message
                    };
                    break;

                case NotificationType.GroupRequest:
                    toast = new GroupRequestToastControl()
                    {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        RenderTransform = new TranslateTransform(),
                        Text = message
                    };
                    break;
            }

            if (toast != null)
            {
                toast.Animate();
                toast.OnRemovedHandler += (x) => _rootGrid.Children.Remove(x);
                _rootGrid.Children.Add(toast);
            }
        }        

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {            
            List<BasePage> pages = new List<BasePage>();
            Utils.FindChildren(pages, this);   
            _connectionPage = (ConnectionPage)pages.Find(item => item.GetType() == typeof(Client.ConnectionPage));
            _connectionPage.OnConnectedHandler += OnConnected;
            _connectionPage.SetEnabled(true);

            _lobbyPage = (LobbyPage)pages.Find(item => item.GetType() == typeof(Client.LobbyPage));
            _lobbyPage.OnEnterGroupChatHandler += OnEnterGroupChat;
            _lobbyPage.OnLogoutHandler += OnLogout;
            _lobbyPage.SetEnabled(false);

            _groupChatPage = (GroupChatPage)pages.Find(item => item.GetType() == typeof(Client.GroupChatPage));
            _groupChatPage.OnLobbyHandler += OnLobby;
            _groupChatPage.SetEnabled(false);


            _rootGrid = Utils.FindChildren(new List<Grid>(), this).FirstOrDefault();
            NotificationService.OnNotificationStaticHandler += OnNotification;
        }

        private void OnLogout()
        {            
            _connectionPage.Open(null);
            _lobbyPage.Close();
            _groupChatPage.Close();
        }

        private void OnLobby()
        {
            _connectionPage.Close();
            _lobbyPage.Open(_connection);
            _groupChatPage.Close();
        }

        public void OnConnected(Connection conn)
        {
            _connection = conn;
            _connectionPage.Close();
            _lobbyPage.Open(conn);
            _groupChatPage.Close();            
        }

        public void OnEnterGroupChat(Group group)
        {
            _connectionPage.Close();
            _lobbyPage.Close();
            _groupChatPage.Open(_connection);
        }

        private void OnDebugKeyPressed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.F1) return;

            NotificationService.OnNotificationStaticHandler?.Invoke(NotificationType.GroupRequest, "My Friend's Group");
        }
    }
}

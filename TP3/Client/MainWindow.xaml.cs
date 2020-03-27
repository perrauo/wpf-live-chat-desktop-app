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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ConnectionPage _connectionPage;
        private LobbyPage _lobbyPage;
        private GroupChatPage _groupChatPage;

        private ToastControl _toastControl;

        private Network.Connection _connection;

        public MainWindow()
        {
            InitializeComponent();            
            this.Loaded += OnMainWindowLoaded;
        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            List<Page> pages = new List<Page>();
            Utils.FindChildren(pages, this);    
            _connectionPage = (ConnectionPage)pages.Find(item => item.GetType() == typeof(Client.ConnectionPage));
            _lobbyPage = (LobbyPage)pages.Find(item => item.GetType() == typeof(Client.LobbyPage));
            _groupChatPage = (GroupChatPage)pages.Find(item => item.GetType() == typeof(Client.GroupChatPage));
            //_connectionPage.SetEnabled(true);
            //_lobbyPage.SetEnabled(false);
            //_groupChatPage.SetEnabled(false);
            //_connectionPage.OnConnectedHandler += OnConnected;

            List<ToastControl> toasts = new List<ToastControl>();
            Utils.FindChildren(toasts, this);
            _toastControl = (ToastControl)toasts.Find(item => item.GetType() == typeof(Client.ToastControl));

            _toastControl.IsToastVisible = true;
        }

        public void OnConnected(Network.Connection conn)
        {
            _connection = conn;
            _connectionPage.SetEnabled(false);
            _lobbyPage.SetEnabled(true);
        }

        private void notificationButton_Click(object sender, RoutedEventArgs e)
        {
            //clear_all();
            //named.Children.Add(new MainNotificationPage());
            //named

        }

    }
}

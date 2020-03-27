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
        private const string RootGridString = "Root";

        private ConnectionPage _connectionPage;
        private LobbyPage _lobbyPage;
        private GroupChatPage _groupChatPage;
        private BaseToastControl _toastControl;
        private Network.Connection _connection;
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
                case NotificationType.GroupRequest:
                    toast = new GroupRequestToastControl()
                    {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        RenderTransform = new TranslateTransform(),
                    };
                    break;
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
            List<Page> pages = new List<Page>();
            Utils.FindChildren(pages, this);   
            _connectionPage = (ConnectionPage)pages.Find(item => item.GetType() == typeof(Client.ConnectionPage));
            _connectionPage.OnConnectedHandler += OnConnected;

            _lobbyPage = (LobbyPage)pages.Find(item => item.GetType() == typeof(Client.LobbyPage));
            _lobbyPage.SetEnabled(false);

            _groupChatPage = (GroupChatPage)pages.Find(item => item.GetType() == typeof(Client.GroupChatPage));
            _groupChatPage.SetEnabled(false);


            _rootGrid = Utils.FindChildren(new List<Grid>(), this).FirstOrDefault();
            NotificationService.OnNotificationStaticHandler += OnNotification;
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

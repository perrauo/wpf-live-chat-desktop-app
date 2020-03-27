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
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : Page
    {
        private Network.Connection _connection;
        private LobbyController _lobbyController = new LobbyController();
        private ListBox _groupsListBox;

        public const string GroupsListBoxString = "GroupsListBox";


        public LobbyPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        public void Open(Network.Connection conn)
        {
            _connection = conn;
            PopulateGroupList();
        }

        private void PopulateGroupList()
        {
            //_groupsListBox.
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            List<ListBox> results = new List<ListBox>();
            Utils.FindChildren(results, this);
            _groupsListBox = results.Find(item => item.Name.Equals(GroupsListBoxString));
        }

    }
}


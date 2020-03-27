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
    public class GroupListBoxItem
    {
        public Model.Group Group { get; set; }

        public string GroupName => Group.GroupName;

        public int NumActiveMembers => Group.NumActiveMembers;

        public int NumMembers => Group.NumMembers;
    }

    /// <summary>
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : BasePage
    {        
        private LobbyController _lobbyController = new LobbyController();
        private ListBox _groupsListBox;

        public Action<Model.Group> OnEnterGroupChatHandler { get; set; }

        public const string GroupsListBoxString = "GroupsListBox";


        public LobbyPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
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

        public void AddGroup(Model.Group group)
        {
            GroupListBoxItem item;
            _groupsListBox.Items.Add(item = new GroupListBoxItem()
            {
                Group = group
            });
        }

        private void OnGroupAddedButtonClicked(object sender, RoutedEventArgs e)
        {
            AddGroup(new Model.Group()
            {
                NumMembers = 48,
                NumActiveMembers = 7,
                GroupName = "Hello World"
            });
        }

        public void OnGroupEnterButtonClicked(object sender, RoutedEventArgs e)
        {            
            Button button = (Button)sender;
            GroupListBoxItem item = (GroupListBoxItem)button.DataContext;
            OnEnterGroupChatHandler?.Invoke(item.Group);
        }

        private void ListBoxItem_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            
        }

        private void GroupsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}


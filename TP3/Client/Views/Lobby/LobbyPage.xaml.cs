using IFT585_TP3.Client.Model;
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

namespace IFT585_TP3.Client
{
    public class GroupListBoxItem
    {
        public Group Group { get; set; }

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

        public Action<Group> OnEnterGroupChatHandler { get; set; }

        public const string GroupsListBoxString = "GroupsListBox";

        public const string CreateGroupQuestionString = "Please enter a group name:";

        public const string DefaultGroupNameString = "My Chat Group";


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

        public void AddGroup(Group group)
        {
            GroupListBoxItem item;
                //lblName.Text = inputDialog.Answer;

            _groupsListBox.Items.Add(item = new GroupListBoxItem()
            {
                Group = group
            });
        }

        private void OnGroupAddedButtonClicked(object sender, RoutedEventArgs e)
        {

            QuestionDialog dialog = new QuestionDialog(CreateGroupQuestionString, DefaultGroupNameString);
            if (dialog.ShowDialog() == true)
            {
                // TODO verify if group exists
                if (!_lobbyController.GroupExists(dialog.Answer))
                {
                    Group group = new Group() { GroupName = dialog.Answer };                    
                    group.AdminUsernames.Add(_connection.Username);
                    group.MemberUsernames.Add(_connection.Username);
                    AddGroup(group);                    
                }
            }
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


using IFT585_TP3.Client.Controllers;
using IFT585_TP3.Common.Reponses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
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
    public enum UserGroupRelationship
    {
        Outsider = 0,
        Pending = 1,
        Member = 2
    }

    public class GroupListBoxItem
    {
        public Group Group { get; private set; }

        public GroupListBoxItem(string username, Group group)
        {
            this.Group = group;

            if (Group.MemberUsernames.Contains(username))
            {
                _groupRelationship = UserGroupRelationship.Member;
            }
            else if (Group.InvitedUsernames.Contains(username))
            {
                _groupRelationship = UserGroupRelationship.Pending;
            }
            else _groupRelationship = UserGroupRelationship.Outsider;
        }

        public UserGroupRelationship _groupRelationship { get; set; }

        public bool IsPending => _groupRelationship == UserGroupRelationship.Pending;
        public bool IsOutsider => _groupRelationship == UserGroupRelationship.Outsider;
        public bool IsMember => _groupRelationship == UserGroupRelationship.Member;

        public string GroupName => Group.GroupName;

        public int NumActiveMembers => Group.NumActiveMembers;

        public int NumMembers => Group.NumMembers;
    }

    /// <summary>
    /// Interaction logic for LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : BasePage
    {
        private LobbyController _lobbyController;
        private ListBox _groupsListBox;
        private Timer _refreshTimer;

        public Action<Group> OnEnterGroupChatHandler { get; set; }

        public const string GroupsListBoxString = "GroupsListBox";

        public const string CreateGroupQuestionString = "Please enter a group name:";

        public const string DefaultGroupNameString = "My Chat Group";

        public const string DefaultUsernameString = "someusername";

        public const string AdminAddUserQuestionString = "Please enter a user name:";

        public const string AdminDeleteUserQuestionString = "Please enter a user name:";

      
        public LobbyPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.ViewDisplayed += LobbyPage_ViewDisplayed;
            this.ViewDiscarded += LobbyPage_ViewDiscarded;

            _refreshTimer = new Timer() { Interval = 5000 };
            _refreshTimer.Elapsed += _refreshTimer_Elapsed;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            List<ListBox> results = new List<ListBox>();
            Utils.FindChildren(results, this);
            _groupsListBox = results.Find(item => item.Name.Equals(GroupsListBoxString));
        }

        private void LobbyPage_ViewDisplayed(object sender, EventArgs e)
        {
            if (_lobbyController == null)
            {
                _lobbyController = new LobbyController(_connection);
            }
            
            PopulateGroupList();

            _refreshTimer.Enabled = true;
        }

        private void LobbyPage_ViewDiscarded(object sender, EventArgs e)
        {
            _refreshTimer.Enabled = false;
        }

        private void _refreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                PopulateGroupList();
            });
        }

        private async Task PopulateGroupList()
        {
            _groupsListBox.Items.Clear();

            var result = await _lobbyController.GetGroups();
            if (result.IsSuccess)
            {
                foreach (var group in result.Value.Groups)
                {
                    _groupsListBox.Items.Add(new GroupListBoxItem(_connection.Username, group));
                }
            }
        }

        private void OnGroupAddedButtonClicked(object sender, RoutedEventArgs e)
        {
            QuestionDialog dialog = new QuestionDialog(CreateGroupQuestionString, DefaultGroupNameString);
            if (dialog.ShowDialog() == true)
            {
                Group group = new Group() { GroupName = dialog.Answer };
                group.AdminUsernames.Add(_connection.Username);
                group.MemberUsernames.Add(_connection.Username);
                group.ConnectedUsernames.Add(_connection.Username);
                AddGroup(_connection.Username, group);
            }
        }

        private async Task AddGroup(string username, Group group)
        {
            var result = await _lobbyController.CreateGroup(group);
            if (result.IsSuccess)
            {
                _groupsListBox.Items.Add(new GroupListBoxItem(username, group));
            }
        }

        private void OnAdminDeleteUserClicked(object sender, RoutedEventArgs e)
        {
            QuestionDialog dialog = new QuestionDialog(AdminDeleteUserQuestionString, DefaultUsernameString);
            if (dialog.ShowDialog() == true)
            {
                DeleteUser(dialog.Answer);
            }
        }

        private async Task DeleteUser(string username)
        {
            await _lobbyController.CreateUser(username);
        }

        private void OnAdminAddUserClicked(object sender, RoutedEventArgs e)
        {

            QuestionDialog dialog = new QuestionDialog(AdminAddUserQuestionString, DefaultUsernameString);
            if (dialog.ShowDialog() == true)
            {
                CreateUser(dialog.Answer);
            }
        }

        private async Task CreateUser(string username)
        {
            await _lobbyController.CreateUser(username);
        }

        public void OnGroupEnterButtonClicked(object sender, RoutedEventArgs e)
        {            
            Button button = (Button)sender;
            GroupListBoxItem item = (GroupListBoxItem)button.DataContext;
            OnEnterGroupChatHandler?.Invoke(item.Group);
        }

        public void OnGroupAcceptButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            GroupListBoxItem item = (GroupListBoxItem)button.DataContext;

            AcceptInvite(item.GroupName);
        }

        private async Task AcceptInvite(string groupName)
        {
            var result = await _lobbyController.AcceptGroupInvite(groupName);
            if (result.IsSuccess)
            {
                PopulateGroupList();
            }
        }

        public void OnGroupDeclineButtonClicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            GroupListBoxItem item = (GroupListBoxItem)button.DataContext;

            RefuseInvite(item.GroupName);
        }

        private async Task RefuseInvite(string groupName)
        {
            var result = await _lobbyController.RefuseGroupInvite(groupName);
            if (result.IsSuccess)
            {
                PopulateGroupList();
            }
        }
    }
}


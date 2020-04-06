using IFT585_TP3.Client.Controllers;
using IFT585_TP3.Common.Reponses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    public class ChatFeedListBoxItem
    {
        public Message Message { get; set; }

        public string Content => Message.Content;

        public string Username => Message.SenderUsername;
    }


    public class MemberListItem
    {
        public string Username { get; set; } = "";

        public bool IsAdmin { get; set; } = false;

        public bool IsConnected { get; set; } = false;
    }


    /// <summary>
    /// Interaction logic for GroupManagementPage.xaml
    /// </summary>
    public partial class GroupChatPage : BasePage
    {
        public const string ChatFeedListBoxString = "ChatFeedListBox";
        public const string ChatFeedScrollViewerString = "ChatFeedScrollViewer";
        public const string MessageInputTextBoxString = "MessageInputTextBox";
        public const string MakeAdminQuestionString = "Please enter who you want to make admin:";
        public const string MakeAdminDefaultString = "My admin";
        public const string InviteQuestionString = "Please enter who you want to invite:";
        public const string InviteDefaultString = "My new member";
        public const string KickOutQuestionString = "Please enter who you want to kick out:";
        public const string KickoutDefaultString = "My enemy";

        private GroupChatController _groupChatController;
        private ScrollViewer _chatFeedScrollViewer;
        private ListBox _chatFeedListBox;
        private TextBox _messageInputTextBox;
        private Timer _refreshTimer;
        private DateTime _lastMessageUpdate = new DateTime();

        public Action OnLobbyHandler { get; set; }

        public Group Group { get; set; }

        public GroupChatPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
            this.ViewDisplayed += GroupChatPage_ViewDisplayed;
            this.ViewDiscarded += GroupChatPage_ViewDiscarded;

            _refreshTimer = new Timer() { Interval = 5000 };
            _refreshTimer.Elapsed += _refreshTimer_Elapsed;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _chatFeedListBox =
                Utils.FindChildren(new List<ListBox>(), this)
                .FirstOrDefault(item => item.Name.Equals(ChatFeedListBoxString));
            _messageInputTextBox =
                Utils.FindChildren(new List<TextBox>(), this)
                .FirstOrDefault(item => item.Name.Equals(MessageInputTextBoxString));
            _chatFeedScrollViewer = Utils.FindChildren(new List<ScrollViewer>(), this)
                .FirstOrDefault(item => item.Name.Equals(ChatFeedScrollViewerString));
        }

        private void GroupChatPage_ViewDisplayed(object sender, EventArgs e)
        {
            if (_groupChatController == null)
            {
                _groupChatController = new GroupChatController(_connection);
            }

            AdminActions.Visibility = Group.AdminUsernames.Contains(_connection.Username) ? Visibility.Visible : Visibility.Collapsed;

            _lastMessageUpdate = new DateTime(); // reset the timstamp to fetch all messages
            _chatFeedListBox.Items.Clear();

            PopulateUsers();
            PopulateMessages();

            _refreshTimer.Enabled = true;
        }

        private void GroupChatPage_ViewDiscarded(object sender, EventArgs e)
        {
            _refreshTimer.Enabled = false;
        }

        private void _refreshTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                PopulateUsers();
                PopulateMessages();
            });
        }

        private async Task PopulateUsers()
        {
            var result = await _groupChatController.GetGroup(Group.GroupName);
            if (result.IsSuccess)
            {
                lvUsers.Items.Clear();
                Group = result.Value;

                foreach (var member in result.Value.MemberUsernames)
                {
                    lvUsers.Items.Add(new MemberListItem()
                    {
                        Username = member,
                        IsAdmin = Group.AdminUsernames.Contains(member),
                        IsConnected = Group.ConnectedUsernames.Contains(member)
                    });
                }

                AdminActions.Visibility = Group.AdminUsernames.Contains(_connection.Username) ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (result.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                // The user got kicked out or the group is now deleted. Navigate back to the lobby.
                OnLobbyHandler?.Invoke();
                NotificationService.OnNotificationStaticHandler?.Invoke($"You dont have access to this group anymore.");
            }
        }

        private async Task PopulateMessages()
        {
            var result = await _groupChatController.GetMessages(Group.GroupName, _lastMessageUpdate);
            _lastMessageUpdate = DateTime.Now;
            if (result.IsSuccess)
            {
                foreach(var message in result.Value.Messages)
                {
                    _chatFeedListBox.Items.Add(new ChatFeedListBoxItem()
                    {
                        Message = message
                    });
                    _chatFeedScrollViewer.ScrollToVerticalOffset(int.MaxValue);
                }
            }
        }

        private async Task SendMessage(string message)
        {
            var result = await _groupChatController.SendMessage(new Message()
            {
                GroupName = Group.GroupName,
                Content = message
            });
            if (result.IsSuccess)
            {
                _messageInputTextBox.Text = "";
                PopulateMessages();
            }
        }

        private void OnMakeAdminButtonClicked(object sender, RoutedEventArgs e)
        {
            QuestionDialog dialog = new QuestionDialog(MakeAdminQuestionString, MakeAdminDefaultString);
            if (dialog.ShowDialog() == true)
            {
                MakeAdmin(dialog.Answer);
            }
        }

        private async Task MakeAdmin(string username)
        {
            await _groupChatController.MakeAdmin(Group.GroupName, username);
        }

        private void OnDeleteGroupButtonClicked(object sender, RoutedEventArgs e)
        {
            DeleteGroup();
        }

        private async Task DeleteGroup()
        {
            var result = await _groupChatController.DeleteGroup(Group.GroupName);
            if (result.IsSuccess)
            {
                OnLobbyHandler?.Invoke();
            }
        }

        private void OnInviteButtonClicked(object sender, RoutedEventArgs e)
        {
            QuestionDialog dialog = new QuestionDialog(InviteQuestionString, InviteDefaultString);
            if (dialog.ShowDialog() == true)
            {
                InviteUser(dialog.Answer);
            }
        }

        private async Task InviteUser(string username)
        {
            await _groupChatController.InviteUser(Group.GroupName, username);
        }

        private void OnKickOutButtonClicked(object sender, RoutedEventArgs e)
        {
            QuestionDialog dialog = new QuestionDialog(KickOutQuestionString, KickoutDefaultString);
            if (dialog.ShowDialog() == true)
            {
                RemoveUser(dialog.Answer);
            }
        }

        private async Task RemoveUser(string username)
        {
            var result = await _groupChatController.RemoveUser(Group.GroupName, username);
            if (result.IsSuccess)
            {
                PopulateUsers();
            }
        }

        private void OnSendButtonClicked(object sender, RoutedEventArgs e)
        {
            SendMessage(_messageInputTextBox.Text);
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;
            SendMessage(_messageInputTextBox.Text);
        }

        private void OnLobbyButtonClicked(object sender, RoutedEventArgs e)
        {
            OnLobbyHandler?.Invoke();
        }

        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;

        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                //lvUsers.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            //lvUsers.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }


        public class SortAdorner : Adorner
        {
            private static Geometry ascGeometry =
                Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

            private static Geometry descGeometry =
                Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

            public ListSortDirection Direction { get; private set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                : base(element)
            {
                this.Direction = dir;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (AdornedElement.RenderSize.Width < 20)
                    return;

                TranslateTransform transform = new TranslateTransform
                    (
                        AdornedElement.RenderSize.Width - 15,
                        (AdornedElement.RenderSize.Height - 5) / 2
                    );
                drawingContext.PushTransform(transform);

                Geometry geometry = ascGeometry;
                if (this.Direction == ListSortDirection.Descending)
                    geometry = descGeometry;
                drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                drawingContext.Pop();
            }
        }
    }
}

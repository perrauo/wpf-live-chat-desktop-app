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
    public class ChatFeedListBoxItem
    {
        public Model.Message Message { get; set; }

        public string Content => Message.Content;

        public string Username => Message.Username;
    }

    /// <summary>
    /// Interaction logic for GroupManagementPage.xaml
    /// </summary>
    public partial class GroupChatPage : BasePage
    {
        public const string ChatFeedListBoxString = "ChatFeedListBox";
        public const string ChatFeedScrollViewerString = "ChatFeedScrollViewer";
        public const string MessageInputTextBoxString = "MessageInputTextBox";

        private ScrollViewer _chatFeedScrollViewer;
        private ListBox _chatFeedListBox;
        private TextBox _messageInputTextBox;

        public Action OnLobbyHandler { get; set; }

        public GroupChatPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
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


        public void AddMessage(Model.Message message)
        {
            _chatFeedListBox.Items.Add(new ChatFeedListBoxItem()
            {
                Message = message
            });

            _chatFeedScrollViewer.ScrollToVerticalOffset(int.MaxValue);
        }

        private void OnSendButtonClicked(object sender, RoutedEventArgs e)
        {
            AddMessage(new Model.Message()
            {
                Content = _messageInputTextBox.Text,
                Username = _connection.Username

            });
        }

        private void OnLobbyButtonClicked(object sender, RoutedEventArgs e)
        {
            OnLobbyHandler?.Invoke();
        }
    }
}

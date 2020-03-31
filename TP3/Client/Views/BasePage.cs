using IFT585_TP3.Client.NetworkFramework;
using System;
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
    public class BasePage : Page
    {
        protected Connection _connection = null;

        public Action OnLogoutHandler { get; set; }

        public virtual void Open(Connection conn)
        {
            Utils.SetEnabled(this, true);
            _connection = conn;
        }

        public virtual void Close()
        {
            Utils.SetEnabled(this, false);
            _connection = null;
        }

        public void OnLogoutButtonClicked(object sender, RoutedEventArgs e)        
        {
            OnLogoutHandler?.Invoke();
        }
    }    
}

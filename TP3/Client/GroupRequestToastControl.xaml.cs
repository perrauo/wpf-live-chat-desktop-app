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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IFT585_TP3.Client
{
    public partial class GroupRequestToastControl : BaseToastControl
    {
        private TextBox _groupNameTextBox = null;

        public GroupRequestToastControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnAcceptButtonClicked(object sender, RoutedEventArgs e)
        {
            OnRemovedHandler?.Invoke(this);
        }

        private void OnDeclineButtonClicked(object sender, RoutedEventArgs e)
        {
            OnRemovedHandler?.Invoke(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _groupNameTextBox = Utils.FindChildren(new List<TextBox>(), this).FirstOrDefault();
        }
    }

}

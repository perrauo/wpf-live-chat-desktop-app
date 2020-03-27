using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client
{
    public class NotificationService
    {
        public static Action<String> OnMessageReceivedHandler;        

        public static void ShowNotification(string message)
        {
            OnMessageReceivedHandler(message);
        }
    }
}

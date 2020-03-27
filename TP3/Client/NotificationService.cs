using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFT585_TP3.Client
{
    public enum NotificationType
    {
        Success,
        Error,
        GroupRequest,
        AdminRequest
    }

    public class NotificationService
    {
        public static Action<NotificationType, string> OnNotificationStaticHandler { get; set; }

        public static void SendNotification(NotificationType type, string message)
        {
            OnNotificationStaticHandler?.Invoke(type, message);
        }
    }
}

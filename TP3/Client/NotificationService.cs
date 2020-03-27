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
        public const string LoginSuccessMessage = "Login Success";
        public const string UnknownErrorMessage = "Unknown Error";

        public static Action<NotificationType, string> OnNotificationStaticHandler { get; set; }
    }
}

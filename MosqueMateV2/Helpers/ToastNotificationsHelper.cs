using Notification.Wpf;

namespace MosqueMateV2.Helpers
{
    public class ToastNotificationsHelper
    {
        /// <summary>
        /// Provides functionality to display toast notifications in a WPF application.
        /// </summary>
        public static void SendNotification(string title,string message, NotificationType type)
        {
            NotificationManager notificationManager = new();
            notificationManager.Show(new NotificationContent
            {
                Title = title,
                Message = message,
                Type =type,    
            });
        }
    }
}

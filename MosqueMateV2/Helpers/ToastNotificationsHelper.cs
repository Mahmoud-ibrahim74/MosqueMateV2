using Notification.Wpf;

namespace MosqueMateV2.Helpers
{
    public class ToastNotificationsHelper
    {
        /// <summary>
        /// Provides functionality to display toast notifications in a WPF application.
        /// </summary>
        public static void SendNotification(
            string title,
            string message,
            TimeSpan duration,
            NotificationType type,
            Action onClick = null,
            Action onClose = null)
        {
            NotificationManager notificationManager = new();
            notificationManager.Show(
                            title:title,
                            message:message,
                            expirationTime:duration,
                            type:type,
                            onClick:onClick,
                            onClose:onClose
                );
        }
    }
}

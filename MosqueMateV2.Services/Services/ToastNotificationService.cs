using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace MosqueMateV2.Services.Services
{
    public class ToastNotificationService
    {
        private readonly Notifier notifier;
        public ToastNotificationService()
        {
            notifier = new Notifier(cfg =>
           {
               cfg.PositionProvider = new WindowPositionProvider(
                   parentWindow: Application.Current.MainWindow,
                   corner: Corner.TopRight,
                   offsetX: 10,
                   offsetY: 10);

               cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                   notificationLifetime: TimeSpan.FromSeconds(5),
                   maximumNotificationCount: MaximumNotificationCount.FromCount(5));

               cfg.Dispatcher = Application.Current.Dispatcher;
           });

        }
    }
}

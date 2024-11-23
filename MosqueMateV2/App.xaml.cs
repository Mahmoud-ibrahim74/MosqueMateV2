using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using Resources;
using System.Globalization;
using System.Windows;
namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static JsonLocalizationService LocalizationService { get; private set; }
        public static string AppLanguage { get; private set; }
        public static DTOPrayerTimesResponse Api_Response { get; set; }
        public static IMP3Player mP3Player { get; set; }
        private RxTaskScheduler _scheduler;

        protected override void OnStartup(StartupEventArgs e)
        {
            mP3Player = new MP3Player();
            // Load localization file
            LocalizationService = new JsonLocalizationService();

            // Set default culture (e.g., "en")
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppLocalization.English);
            AppLanguage = Thread.CurrentThread.CurrentUICulture.Name;

            _scheduler = new RxTaskScheduler();

            // Start the background task directly in the Start method
            _scheduler.Start(() =>
            {
                // Direct task logic here
                Console.WriteLine($"Task executed at {DateTime.Now}");
                return Task.CompletedTask;  // Return a completed Task
            }, TimeSpan.FromSeconds(1)); // Task will be triggered every 5 seconds

            base.OnStartup(e);

        }
        protected override void OnExit(ExitEventArgs e)
        {
            // Stop and clean up the scheduler
            _scheduler.Stop();

            base.OnExit(e);
        }
    }

}

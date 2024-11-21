using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            mP3Player = new MP3Player();
            // Load localization file
            LocalizationService = new JsonLocalizationService();

            // Set default culture (e.g., "en")
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppLocalization.English);
            AppLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            base.OnStartup(e);

        }
    }

}

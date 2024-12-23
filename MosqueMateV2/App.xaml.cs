using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
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

        public App()
        {
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            #region New Instance
            mP3Player = new MP3Player();
            LocalizationService = new();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppSettings.Default.Lang);
            #endregion


            #region App-StartUp
            if (AppSettings.Default.AutoStartUp)
                AppHelper.AddApplicationToStartup();
            else
                AppHelper.RemoveApplicationFromStartup();
            #endregion

            AppLanguage = Thread.CurrentThread.CurrentUICulture.Name;


            base.OnStartup(e);

        }
        protected override void OnExit(ExitEventArgs e)
        {

        }
    }

}

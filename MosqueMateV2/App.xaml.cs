using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using Resources;
using System.Globalization;
using System.IO;
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
        private static string fontPath { get; set; }
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
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppLocalization.Arabic);
            #endregion
            Application.Current.ThemeMode = ThemeMode.System;
            AppLanguage = Thread.CurrentThread.CurrentUICulture.Name;
            if (AppLanguage == AppLocalization.Arabic)
                fontPath = AppHelper.ChangeAppFont(FontResources.alfont_com_ArabicPoetry_Medium);

            base.OnStartup(e);

        }
        protected override void OnExit(ExitEventArgs e)
        {
            if (File.Exists(fontPath))
                File.Delete(fontPath);
            base.OnExit(e);
        }
    }

}

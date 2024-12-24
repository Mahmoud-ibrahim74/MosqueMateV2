using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using MosqueMateV2.Service.IServices;
using MosqueMateV2.Service.Services;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class LiveStream : Window
    {
        string _Url { get; set; }
        public LiveStream(string url)
        {
            #region SetTheme
            var currentTheme = AppSettings.Default.themeMode.ToThemeMode();
            this.ThemeMode = currentTheme;
            #endregion
            InitializeComponent();
            this._Url = url;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            liveSource.Dispose();
            AppHelper.GoHome();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = App.LocalizationService[SD.Localization.QuranLive];
            this.zekrTitle.Text = App.LocalizationService[SD.Localization.QuranLive];
            liveSource.DefaultBackgroundColor = System.Drawing.Color.Transparent;
            liveSource.Source = new Uri(this._Url);
        }
    }
}

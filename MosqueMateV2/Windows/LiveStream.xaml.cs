using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using System.Windows;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class LiveStream : Window
    {
        string _StreamUrl { get; set; }
        public LiveStream(string url)
        {
            #region SetTheme
            this.ThemeMode = OSHelper.GetWindowsTheme();
            #endregion
            InitializeComponent();
            this._StreamUrl = url;
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
            liveSource.Source = new Uri(this._StreamUrl);
        }
    }
}

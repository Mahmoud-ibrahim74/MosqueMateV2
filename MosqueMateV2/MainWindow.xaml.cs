using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Helpers;
using MosqueMateV2.Helpers.AppHelpers;
using Newtonsoft.Json;
using Resources;
using System.Text;
using System.Windows;
using System.Windows.Input;
using XamlAnimatedGif;

namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string apiContent { get; set; }
        public DTOPrayerTimesResponse objDeserialize { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ApiClient.Configure("cairo", "egypt", 2);
            this.Title = App.LocalizationService[AppLocalization.MainMenu];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            this.IsEnabled = false;
            TaskHelper.RunBackgroundTaskOnUI(
                 backgroundTask: () => ApiClient.GetAsync(),
                 onSuccess: result =>
                 {
                     apiContent = result;
                     this.IsEnabled = true;
                     objDeserialize = JsonConvert.DeserializeObject<DTOPrayerTimesResponse>(apiContent);
                     RenderWindowWithData();
                 });
        }
        private void RenderWindowWithData()
        {
            this.Focusable = true;
            this.Focus();
            this.FlowDirection = App.AppLanguage == AppLocalization.Arabic ?
                FlowDirection.RightToLeft :
                FlowDirection.LeftToRight;

            #region hijriDateBuilder
            StringBuilder hijriDate = new();
            hijriDate.Append(objDeserialize.Data.Date.Hijri.Day);
            hijriDate.Append(" - ");
            hijriDate.Append(App.AppLanguage == AppLocalization.Arabic ?
                                            objDeserialize.Data.Date.Hijri.Weekday.Arabic :
                                            objDeserialize.Data.Date.Hijri.Weekday.English
                            );
            hijriDate.Append(" ");
            hijriDate.Append(App.AppLanguage == AppLocalization.Arabic ?
                                objDeserialize.Data.Date.Hijri.Month.Arabic :
                                objDeserialize.Data.Date.Hijri.Month.English
                );
            hijriDate.Append(" ");
            hijriDate.Append(objDeserialize.Data.Date.Hijri.Year);
            #endregion

            #region WelcomeBuilder
            StringBuilder WelcomeBuilder = new();
            WelcomeBuilder.Append(App.LocalizationService[AppLocalization.WeclomeApp]);
            WelcomeBuilder.Append(" , ");
            WelcomeBuilder.Append(DateTimeHelper.PrintGreeting());
            #endregion

            this.welcomeLBL.Content = WelcomeBuilder.ToString();
            this.hijiriDateLBL.Content = hijriDate.ToString();
        }

        private void Window_KeyDown(object sender,KeyEventArgs e)
        {
            // Check if Ctrl, Shift, and D are pressed together
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control &&
                (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift &&
                e.Key == Key.D)
            {
                
            }
        }
    }
}
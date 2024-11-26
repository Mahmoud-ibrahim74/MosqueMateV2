using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using Newtonsoft.Json;
using Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using XamlAnimatedGif;

namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string apiContent { get; set; } = string.Empty;
        public List<PrayerSlide> PrayerSlidesData { get; set; }
        RxTaskManger rxTaskManger;
        public MainWindow()
        {
            InitializeComponent();
            rxTaskManger = new();
            ApiClient.Configure("cairo", "egypt", 8);
            PrayerSlidesData = [];
            AnimationBehavior.SetSourceUri(Loader, new Uri("pack://application:,,,/Assets/loader.gif"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loader.Visibility = Visibility.Visible;
            this.IsEnabled = false;
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: () => ApiClient.GetAsync(),
                 onSuccess: result =>
                 {
                     apiContent = result;
                     this.IsEnabled = true;
                     this.Loader.Visibility = Visibility.Hidden;
                     this.Loader.Source = null;
                     App.Api_Response = JsonConvert.DeserializeObject<DTOPrayerTimesResponse>(apiContent);
                     RenderWindowWithData();
                 },
                 retryNumber: 2,
                 () => // handle an error
                 {

                 });

        }
        private void RenderWindowWithData()
        {
            this.Focusable = true;
            this.Focus();
            this.Title = App.LocalizationService[AppLocalization.AppName];
            this.topGrid.FlowDirection = App.AppLanguage == AppLocalization.Arabic ? 
                FlowDirection.RightToLeft : 
                FlowDirection.LeftToRight;

            this.centerGrid.FlowDirection = App.AppLanguage == AppLocalization.Arabic ?
                FlowDirection.RightToLeft :
                FlowDirection.LeftToRight;

            BindingCarusel();
            BindingBottomPanel();

            #region hijriDateBuilder
            var hijriDate = StringHelper.AppendString(
                 App.Api_Response.Data.Date.Hijri.Day,
                 " - ",
                 App.AppLanguage == AppLocalization.Arabic ?
                 App.Api_Response.Data.Date.Hijri.Weekday.Arabic :
                 App.Api_Response.Data.Date.Hijri.Weekday.English,
                 " ",
                 App.AppLanguage == AppLocalization.Arabic ?
                 App.Api_Response.Data.Date.Hijri.Month.Arabic :
                 App.Api_Response.Data.Date.Hijri.Month.English,
                 " ",
                 App.Api_Response.Data.Date.Hijri.Year);
            #endregion

            #region WelcomeBuilder
            var WelcomeBuilder = StringHelper.AppendString(
                App.LocalizationService[AppLocalization.WeclomeApp],
                " , ",
                DateTimeHelper.PrintGreeting()
                );
            #endregion

            #region WindowControls

            this.welcomeLBL.Content = WelcomeBuilder.ToString();
            this.hijiriDateLBL.Content = hijriDate.ToString();
            this.miladiDateLBL.Content = DateTime.Now.ToLocalizedDate(App.AppLanguage);
            #endregion

            rxTaskManger.StartUITaskScheduler(async () => await Task.CompletedTask, TimeSpan.FromSeconds(1), UpdateNextPrayerLabel);

        }

        private void BindingCarusel()
        {
            try
            {
                if (App.Api_Response is not null)
                {
                    var prayers = new (PrayerEnum Prayer, DateTime Timing)[]
                    {
                    (PrayerEnum.Fajr, App.Api_Response.Data.Timings.Fajr),
                    (PrayerEnum.Sunrise, App.Api_Response.Data.Timings.Sunrise),
                    (PrayerEnum.Dhuhr, App.Api_Response.Data.Timings.Dhuhr),
                    (PrayerEnum.Asr, App.Api_Response.Data.Timings.Asr),
                    (PrayerEnum.Maghrib, App.Api_Response.Data.Timings.Maghrib),
                    (PrayerEnum.Isha, App.Api_Response.Data.Timings.Isha)
                    };

                    var orderedPrayers = prayers
                        .Where(p => p.Timing > DateTime.Now)
                        .OrderBy(p => p.Timing)
                        .Concat(prayers.Where(p => p.Timing <= DateTime.Now))
                        .ToArray();


                    foreach (var prayer in orderedPrayers)
                    {
                        PrayerSlidesData.Add(new()
                        {
                            CurrentPrayerName = App.LocalizationService[prayer.Prayer.ToString()],
                            CurrentPrayerTime = prayer.Timing.ToString("hh:mm tt"),
                            ImagePath = $"pack://application:,,,/Assets/{prayer.Prayer}.jpg"
                        });
                    }
                    this.DataContext = this;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void BindingBottomPanel()
        {
            var quranTxt = QuranBtn.Template.FindName("quranTxt", QuranBtn) as TextBlock;
            var azkarTxt = azkarBtn.Template.FindName("azkarTxt", azkarBtn) as TextBlock;
            var hadithTxt = hadithBtn.Template.FindName("hadithTxt", hadithBtn) as TextBlock;
            var prayerLearningTxt = prayerLearningBtn.Template.FindName("prayerLearningTxt", prayerLearningBtn) as TextBlock;
           
            if (quranTxt is not null)
                quranTxt.Text = App.LocalizationService[AppLocalization.Quran];
            if (azkarTxt is not null)
                azkarTxt.Text = App.LocalizationService[AppLocalization.Azkar];
            if (hadithTxt is not null)
                hadithTxt.Text = App.LocalizationService[AppLocalization.Hadith];
            if (prayerLearningTxt is not null)
                prayerLearningTxt.Text = App.LocalizationService[AppLocalization.PrayerLearning];

        }

        public void UpdateNextPrayerLabel()
        {
            var nextAdhanEnum = AdhanHelper.GetNextAdhan(App.Api_Response.Data.Timings);

            var localizationNextAdhan = App.LocalizationService[nextAdhanEnum.ToString() ??
                PrayerEnum.Fajr.ToString()];
            var nextPrayer = AdhanHelper.GetTimeLeftForNextAdhan(App.Api_Response.Data.Timings) ??
                TimeSpan.MinValue;

            nextPrayerLBL.Content = StringHelper.AppendString(
                  App.LocalizationService[AppLocalization.NextPrayer],
                  " ",
                  $"{nextPrayer.Hours}:{nextPrayer.Minutes}:{nextPrayer.Seconds}",
                  "\t(" + localizationNextAdhan + ")"
                );

            BindingCarusel();
            AlertForPrayer(nextPrayer);
        }


        private void CloseSlider_Click(object sender, RoutedEventArgs e)
        {
            Storyboard slideOut = (Storyboard)FindResource("SlideOutAnimation");
            slideOut.Begin(SlidingPanel);
            toggleSidebar.IsChecked = false;
        }

        private void AlertForPrayer(TimeSpan timeLeft)
        {
            if (AdhanHelper.IsAlertForNextAdhan)
            {
                var alertMsg = StringHelper.AppendString(
                    App.LocalizationService[AppLocalization.PrayerLeft],
                                " ",
                                timeLeft.Minutes.ToString(), 
                                " ",
                                App.LocalizationService[AppLocalization.Minutes]);

                ToastNotificationsHelper.
                SendAlertPrayerNotification(
                App.LocalizationService[AppLocalization.Alert],
                alertMsg,
                Notification.Wpf.NotificationType.Warning);

                App.mP3Player.Stop();
                var file = MediaResources.GetAdhanFiles().
                    Where(x => x.Key.Contains("prayerNow")).
                    Select(x => x.Value).
                    FirstOrDefault();
                App.mP3Player.Play(file);
            }
        }
        private void toggleSidebar_Click(object sender, RoutedEventArgs e)
        {

            if (toggleSidebar.IsChecked == true)
            {
                Storyboard slideIn = (Storyboard)FindResource("SlideInAnimation");
                slideIn.Begin(SlidingPanel);
            }
            else if (toggleSidebar.IsChecked == false)
            {
                Storyboard slideOut = (Storyboard)FindResource("SlideOutAnimation");
                slideOut.Begin(SlidingPanel);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
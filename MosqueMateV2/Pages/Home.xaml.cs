using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Home : Page
    {

        public string apiContent { get; set; } = string.Empty;
        public List<PrayerSlide> PrayerSlidesData { get; set; }
        RxTaskManger rxTaskManger;
        public Home()
        {
            InitializeComponent();
            rxTaskManger = new();
            var method = (int)EnumHelper<CalculationMethods>.GetEnumValue(AppSettings.Default.method);
            ApiClient.Configure(AppSettings.Default.country.ToLowerInvariant(),
                AppSettings.Default.city.ToLowerInvariant(),
                method);
            PrayerSlidesData = [];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loader.Visibility = Visibility.Visible;
            IsEnabled = false;
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: () => ApiClient.GetAsync(),
                 onSuccess: result =>
                 {
                     apiContent = result;
                     IsEnabled = true;
                     Loader.Visibility = Visibility.Hidden;
                     App.Api_Response = JsonConvert.DeserializeObject<DTOPrayerTimesResponse>(apiContent);
                     RenderWindowWithData();
                 },
                 retryNumber: 5,
                 () => // handle an error
                 {

                 });

        }
        private void RenderWindowWithData()
        {
            Focusable = true;
            Focus();
            Title = App.LocalizationService[AppLocalization.AppName];

            topGrid.FlowDirection = App.AppLanguage == AppLocalization.Arabic ?
                                       FlowDirection.RightToLeft :
                                       FlowDirection.LeftToRight;
            centerGrid.FlowDirection = App.AppLanguage == AppLocalization.Arabic ?
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

            welcomeLBL.Content = WelcomeBuilder.ToString();
            hijiriDateLBL.Content = hijriDate.ToString();
            miladiDateLBL.Content = DateTime.Now.ToLocalizedDate(App.AppLanguage);
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
                    DataContext = this;
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
            if (App.Api_Response is not null)
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
                      "  ( " + localizationNextAdhan + " )"
                    );

                BindingCarusel();
                AlertForPrayer(nextPrayer);
            }
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


                ToastNotificationsHelper.SendNotification(
                            title: App.LocalizationService[AppLocalization.Alert],
                            message: alertMsg,
                            duration: new TimeSpan(0, 0, AppLocalization.NotificatonDuration),
                            onClose: () =>
                            {
                                App.mP3Player.Stop();
                                App.mP3Player.Play(MediaResources.prayerNow);
                            },
            type: Notification.Wpf.NotificationType.Warning);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
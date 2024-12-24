using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Extensions;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using MosqueMateV2.Windows;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
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

        public List<PrayerSlide> PrayerSlidesData { get; set; }
        RxTaskManger rxTaskManger;
        ApiClient api { get; set; }
        public Home()
        {
            InitializeComponent();
            rxTaskManger = new();
            var method = (int)EnumHelper<CalculationMethods>.GetEnumValue(AppSettings.Default.Method);

            api = new ApiClient(
                Country: AppSettings.Default.Country.ToLowerInvariant()
                , City: AppSettings.Default.City.ToLowerInvariant(),
                method: method);


            PrayerSlidesData = [];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loader.Visibility = Visibility.Visible;
            IsEnabled = false;
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => api.GetAsync(),
                 onSuccess: result =>
                 {
                     if (result is not null)
                     {
                         App.Api_Response = JsonConvert.DeserializeObject<DTOPrayerTimesResponse>(result);
                     }
                     IsEnabled = true;
                     Loader.Visibility = Visibility.Hidden;
                     RenderWindowWithData();
                 },
                 retryNumber: 5,
                 () => // handle an error
                 {
                     Loader.Visibility = Visibility.Collapsed;
                 });

        }
        private void RenderWindowWithData()
        {
            Focusable = true;
            Focus();
            Title = App.LocalizationService[SD.Localization.AppName];

            topGrid.FlowDirection = App.AppLanguage == SD.Localization.Arabic ?
                                       FlowDirection.RightToLeft :
                                       FlowDirection.LeftToRight;
            centerGrid.FlowDirection = App.AppLanguage == SD.Localization.Arabic ?
                FlowDirection.RightToLeft :
                FlowDirection.LeftToRight;

            BindingCarusel();
            BindingBottomPanel();
            if (App.Api_Response is not null)
            {
                #region hijriDateBuilder

                var hijriDate = StringExtenstion.AppendString(
                     App.Api_Response.Data.Date.Hijri.Day,
                     " - ",
                     App.AppLanguage == SD.Localization.Arabic ?
                     App.Api_Response.Data.Date.Hijri.Weekday.Arabic :
                     App.Api_Response.Data.Date.Hijri.Weekday.English,
                     " ",
                     App.AppLanguage == SD.Localization.Arabic ?
                     App.Api_Response.Data.Date.Hijri.Month.Arabic :
                     App.Api_Response.Data.Date.Hijri.Month.English,
                     " ",
                     App.Api_Response.Data.Date.Hijri.Year);
                #endregion

                #region WelcomeBuilder
                var WelcomeBuilder = StringExtenstion.AppendString(
                    App.LocalizationService[SD.Localization.WeclomeApp],
                    " , ",
                    DateTimeHelper.PrintGreeting()
                    );
                #endregion

                #region WindowControls

                welcomeLBL.Content = WelcomeBuilder.ToString();
                hijiriDateLBL.Content = hijriDate.ToString();
                miladiDateLBL.Content = DateTime.Now.ToLocalizedDate(App.AppLanguage);
                #endregion
            }

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
            var quizTxt = quizBtn.Template.FindName("quizTxt", quizBtn) as TextBlock;
            var prayerLearningTxt = prayerLearningBtn.Template.FindName("prayerLearningTxt", prayerLearningBtn) as TextBlock;

            if (quranTxt is not null)
                quranTxt.Text = App.LocalizationService[SD.Localization.ContinueReading];
            if (azkarTxt is not null)
                azkarTxt.Text = App.LocalizationService[SD.Localization.ZekrReminder];
            if (quizTxt is not null)
                quizTxt.Text = App.LocalizationService[SD.Localization.TestYourSelf];
            if (prayerLearningTxt is not null)
                prayerLearningTxt.Text = App.LocalizationService[SD.Localization.PrayerLearningChildren];

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


                nextPrayerLBL.Content = StringExtenstion.AppendString(
                      App.LocalizationService[SD.Localization.NextPrayer],
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
                var alertMsg = StringExtenstion.AppendString(
                    App.LocalizationService[SD.Localization.PrayerLeft],
                                " ",
                                timeLeft.Minutes.ToString(),
                                " ",
                                App.LocalizationService[SD.Localization.Minutes]);


                ToastNotificationsHelper.SendNotification(
                            title: App.LocalizationService[SD.Localization.Alert],
                            message: alertMsg,
                            duration: new TimeSpan(0, 0, SD.Localization.NotificatonDuration),
                            onClose: () =>
                            {
                                App.mP3Player.Stop();
                                App.mP3Player.Play(MediaResources.prayerNow);
                            },
            type: Notification.Wpf.NotificationType.Notification);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void QuranBtn_Click(object sender, RoutedEventArgs e)
        {
            new QuranModalPopup(AppSettings.Default.ContinueReading).ShowModal();
        }

        private void azkarBtn_Click(object sender, RoutedEventArgs e)
        {
            new AdhkarModalPopup(new Random().Next(1, 100)).ShowModal();
        }

        private void prayerLearningBtn_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.NavigateToSpecificPage(new PrayerLearning());
        }

        private void quizBtn_Click(object sender, RoutedEventArgs e)
        {
            AppHelper.NavigateToSpecificPage(new HistoricQuestions());

        }
    }
}
using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Helpers;
using MosqueMateV2.Helpers.AppHelpers;
using Newtonsoft.Json;
using Resources;
using System.Text;
using System.Windows;
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

        public MainWindow()
        {
            InitializeComponent();
            ApiClient.Configure("cairo", "egypt", 2);
            PrayerSlidesData =
                   [
                        new(){
                            id = 1,
                            ImagePath = "pack://application:,,,/Assets/Fajr.png",
                            CurrentPrayerName = string.Empty,
                            CurrentPrayerTime = string.Empty,
                        },
                        new(){
                            id = 2,
                            ImagePath = "pack://application:,,,/Assets/Sunrise.png",
                            CurrentPrayerName = string.Empty,
                            CurrentPrayerTime = string.Empty,
                        },
                        new(){
                            id = 3,
                            ImagePath = "pack://application:,,,/Assets/Dhur.jpg",
                            CurrentPrayerName = string.Empty,
                            CurrentPrayerTime = string.Empty,
                        },
                        new(){
                            id=4,
                            ImagePath = "pack://application:,,,/Assets/Asr.jpg",
                            CurrentPrayerName = string.Empty,
                            CurrentPrayerTime = string.Empty,
                        },
                        new(){
                            id = 5,
                            ImagePath = "pack://application:,,,/Assets/Magrib.jpg",
                            CurrentPrayerName = string.Empty,
                            CurrentPrayerTime = string.Empty,
                        },
                        new(){
                            id=6,
                            ImagePath = "pack://application:,,,/Assets/Asha.png",
                            CurrentPrayerName = string.Empty,
                            CurrentPrayerTime = string.Empty,
                        },
                    ];
            AnimationBehavior.SetSourceUri(Loader, new Uri("pack://application:,,,/Assets/loader.gif"));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var adhan = MediaResources.GetAdhanFiles();
            //App.mP3Player.Play(adhan.Values.Skip(5).FirstOrDefault());
            this.Loader.Visibility = Visibility.Visible;
            this.IsEnabled = false;
            TaskHelper.RunBackgroundTaskOnUI(
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
            this.FlowDirection = App.AppLanguage == AppLocalization.Arabic ?
                                                    FlowDirection.RightToLeft :
                                                    FlowDirection.LeftToRight;


            BindingCarusel();

            #region hijriDateBuilder
            StringBuilder hijriDate = new();
            hijriDate.Append(App.Api_Response.Data.Date.Hijri.Day);
            hijriDate.Append(" - ");
            hijriDate.Append(App.AppLanguage == AppLocalization.Arabic ?
                                            App.Api_Response.Data.Date.Hijri.Weekday.Arabic :
                                            App.Api_Response.Data.Date.Hijri.Weekday.English
                            );
            hijriDate.Append(" ");
            hijriDate.Append(App.AppLanguage == AppLocalization.Arabic ?
                                App.Api_Response.Data.Date.Hijri.Month.Arabic :
                                App.Api_Response.Data.Date.Hijri.Month.English
                );
            hijriDate.Append(" ");
            hijriDate.Append(App.Api_Response.Data.Date.Hijri.Year);
            #endregion

            #region WelcomeBuilder
            StringBuilder WelcomeBuilder = new();
            WelcomeBuilder.Append(App.LocalizationService[AppLocalization.WeclomeApp]);
            WelcomeBuilder.Append(" , ");
            WelcomeBuilder.Append(DateTimeHelper.PrintGreeting());
            #endregion

            #region WindowControls
            this.welcomeLBL.Content = WelcomeBuilder.ToString();
            this.hijiriDateLBL.Content = hijriDate.ToString();
            this.timeNow.Source = new BitmapImage(new
                Uri(DateTimeHelper.
                GetNightOfDay(App.Api_Response.Data.Timings.Maghrib))
                );
            toggleAdhan.Content = App.LocalizationService[AppLocalization.Pause];
            #endregion

        }

        private void BindingCarusel()
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

                // Update PrayerSlidesData using the prayer array
                for (int i = 0; i < prayers.Length; i++)
                {
                    PrayerSlidesData[i].CurrentPrayerName = App.LocalizationService[prayers[i].Prayer.ToString()];
                    PrayerSlidesData[i].CurrentPrayerTime = prayers[i].Timing.ToString("hh:mm tt");
                }
                this.DataContext = this;
            }
        }

        private void toggleAdhan_Click(object sender, RoutedEventArgs e)
        {
            if (toggleAdhan.IsChecked == true)
            {
                toggleAdhan.Content = App.LocalizationService[AppLocalization.Play];
                App.mP3Player.Pause();
            }
            else if (toggleAdhan.IsChecked == false)
            {
                toggleAdhan.Content = App.LocalizationService[AppLocalization.Pause];
                App.mP3Player.Play();
            }
        }
    }
}
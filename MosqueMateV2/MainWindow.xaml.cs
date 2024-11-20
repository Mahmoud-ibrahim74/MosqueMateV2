using MosqueMateV2.CustomUserControls;
using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Helpers;
using MosqueMateV2.Helpers.AppHelpers;
using Newtonsoft.Json;
using Resources;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using XamlAnimatedGif;

namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string apiContent { get; set; } = string.Empty;
        public ObservableCollection<PrayerSlide> PrayerSlidesData { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ApiClient.Configure("cairo", "egypt", 2);
            PrayerSlidesData =
                   [
                        new(){ ImagePath = "pack://application:,,,/Assets/Dhur.jpg", Title = ".....", Description = "......" },
                        new(){ ImagePath = "pack://application:,,,/Assets/Asr.jpg", Title = "....", Description = "......" },
                        new(){ ImagePath = "pack://application:,,,/Assets/Magrib.jpg", Title = "....", Description = "......" },
                        new(){ ImagePath = "pack://application:,,,/Assets/Asha.png", Title = ".....", Description = "......." },
                        new(){ ImagePath = "pack://application:,,,/Assets/Fajr.png", Title = ".....", Description = "....." },
                        new(){ ImagePath = "pack://application:,,,/Assets/shrouq.png", Title = ".....", Description = "......" },
                    ];
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
                     App.Api_Response = JsonConvert.DeserializeObject<DTOPrayerTimesResponse>(apiContent);
                     RenderWindowWithData();
                 },
                 () => // handle an error
                 {
                 });
        }
        private void RenderWindowWithData()
        {
            this.Focusable = true;
            this.Focus();
            this.Title = App.LocalizationService[AppLocalization.MainMenu];
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

            this.welcomeLBL.Content = WelcomeBuilder.ToString();
            this.hijiriDateLBL.Content = hijriDate.ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if Ctrl, Shift, and D are pressed together
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control &&
                (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift &&
                e.Key == Key.D)
            {

            }
        }


        private void BindingCarusel()
        {
            if (App.Api_Response is not null)
            {
                var newPrayerSlidesData = PrayerSlidesData.Select(x => new PrayerSlide
                {

                }).ToList();
                DataContext = this;
            }
        }
    }
}
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Extensions;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    public partial class Setting : Page
    {

        public string apiContent { get; set; } = string.Empty;
        IJsonCountryRepository jsonCountry;
        IJsonCityRepository jsonCity;
        IResourceManagerRepository resourceManager;
        RxTaskManger rxTaskManger;
        public Setting()
        {
            InitializeComponent();
            rxTaskManger = new();
            jsonCountry = new JsonCountryRepository();
            jsonCity = new JsonCityRepository();
            resourceManager = new ResourceManagerRepository(ResourceTypeEnum.MediaResources);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            #region Labels
            this.Title = App.LocalizationService[AppLocalization.Settings];
            this.reminderTimeLBL.Content = App.LocalizationService[AppLocalization.ReminderTime];
            this.hoursLBL.Content = App.LocalizationService[AppLocalization.Hours];
            this.minutesLBL.Content = App.LocalizationService[AppLocalization.Minutes];
            this.minutesLBL.Content = App.LocalizationService[AppLocalization.Minutes];
            this.countryLBL.Content = App.LocalizationService[AppLocalization.countryLBL];
            this.cityLBL.Content = App.LocalizationService[AppLocalization.cityLBL];
            this.regionLBL.Content = App.LocalizationService[AppLocalization.regionLBL];
            this.LanguageLBL.Content = App.LocalizationService[AppLocalization.Language];
            this.calcLBL.Content = App.LocalizationService[AppLocalization.calculationMethod];
            this.calcLBL.Content = App.LocalizationService[AppLocalization.calculationMethod];
            this.startupToggleLBL.Content = App.LocalizationService[AppLocalization.autoStartUp];
            this.adhanLBL.Content = App.LocalizationService[AppLocalization.Muezzin];
            this.adhanFajrLBL.Content = App.LocalizationService[AppLocalization.adhanFajr];
            this.notificationLBL.Content = App.LocalizationService[AppLocalization.notificationLBL];
            this.save.Content = App.LocalizationService[AppLocalization.Save];

            #endregion

            this.countryBox.IsEnabled = false;
            this.adhanBox.IsEnabled = false;
            this.adhanFajrBox.IsEnabled = false;

            #region BackgroundWorker-Area
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: () => jsonCountry.GetAllCountiresAsync(),
                         onSuccess: result =>
                         {
                             this.countryBox.IsEnabled = true;
                             this.countryBox.ItemsSource = result;
                             FillCalc();
                         },
                         retryNumber: 2,
                         () => // handle an error
                         {

                         });
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: () => resourceManager.GetAllResourcesInfoFromResxAsync(),
                         onSuccess: result =>
                         {
                             this.adhanBox.IsEnabled = true;
                             this.adhanFajrBox.IsEnabled = true;
                             this.adhanBox.ItemsSource = result.
                                                          Select(x => x.Name).
                                                          Where(x => !x.Contains(PrayerEnum.Fajr.ToString()) && !x.Contains("Now"))
                                                         .AddSpacesBetweenWords();
                             this.adhanFajrBox.ItemsSource = result.
                                                          Select(x => x.Name).
                                                          Where(x => x.Contains(PrayerEnum.Fajr.ToString()) && !x.Contains("Now"))
                                                         .AddSpacesBetweenWords();
                         },
                         retryNumber: 2,
                         () => // handle an error
                         {

                         });
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: () => LoadProfile(),
                         onSuccess: result =>
                         {

                         },
                         retryNumber: 2,
                         () => // handle an error
                         {

                         });
            #endregion

        }
        private void hoursTxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !NumberHelper.IsTextNumeric(e.Text);
        }
        private void countryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (countryBox.SelectedValue is not null)
            {
                var city = countryBox.SelectedValue.ToString() ?? string.Empty;
                var res = jsonCity.SearchOnCountry(city);
                ReFillCityBox(res);
            }
        }
        private void ReFillCityBox(List<string> data)
        {
            if (data is not null && data.Any())
            {
                cityBox.ItemsSource = null; // Clear the existing ItemsSource
                cityBox.ItemsSource = data; // Set the new ItemsSource
            }
            else
            {
                cityBox.ItemsSource = null; // Clear the cityBox if no data is available
            }
        }
        private void FillCalc()
        {
            var data = EnumHelper<CalculationMethods>.ConvertEnumToFormattedList();
            if (data is not null && data.Any())
            {
                calcBox.ItemsSource = data;
            }
        }
        private void calcBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calcBox.SelectedValue is not null)
            {
                var selected = calcBox.SelectedValue as string ?? string.Empty;
                var res = EnumHelper<CalculationMethods>.GetEnumValue(selected);
            }
        }
        private void notificationToggle_Click(object sender, RoutedEventArgs e)
        {
            if (notificationToggle.IsChecked is true)
            {
                if(Application.Current.ThemeMode == ThemeMode.Light)
                {
                    notificationAlertTxt.FlowDirection = App.AppLanguage == AppLocalization.Arabic ? 
                        FlowDirection.RightToLeft : FlowDirection.LeftToRight;
                    notificationAlertTxt.Foreground = new SolidColorBrush(Colors.DarkOrange);
                }
                else
                {
                    notificationAlertTxt.FlowDirection  = FlowDirection.LeftToRight;    
                }
                    notificationAlertTxt.FlowDirection = App.AppLanguage == AppLocalization.Arabic ?
                    FlowDirection.RightToLeft : FlowDirection.LeftToRight;
                notificationAlertTxt.Text = App.LocalizationService[AppLocalization.notificationDesc];
            }
            else if (notificationToggle.IsChecked is false)
            {
                notificationAlertTxt.Text = string.Empty;
            }
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveProfile();
        }
        private void SaveProfile()
        {
            #region Remainder-Area
            var hours = int.Parse(hoursTxt.Text);
            var min = int.Parse(minutesTxt.Text);
            var remainderTime = new TimeSpan(hours, min, 0);
            #endregion

            #region Region-Area
            var country = countryBox.SelectedValue as string;
            var city = cityBox.SelectedValue as string;
            var calc = calcBox.SelectedValue as string;
            #endregion

            #region Lang-Area


            string lang = arabicRadioBtn.IsChecked == true ? AppLocalization.Arabic :
                          englishRadioBtn.IsChecked == true ? AppLocalization.English :
                          frenshRadioBtn.IsChecked == true ? AppLocalization.Frensh :
                          AppLocalization.Arabic;
            #endregion

            #region AutoStart
            var autoStart = startupTogle.IsChecked;
            var notification = notificationToggle.IsChecked;
            #endregion

            #region TheMuzzein
            var adhan = adhanBox.SelectedValue as string;
            var adhanFajr = adhanFajrBox.SelectedValue as string;

            #endregion


            #region Set-Value-Settings
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Lang)] = lang;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.TimeRemainder)] = remainderTime;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.country)] = country;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.city)] = city;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.method)] = calc;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.AutoStartUp)] = autoStart;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Adhan)] = adhan;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.AdhanFajr)] = adhanFajr;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.NotificationEnabled)] = notification;
            #endregion


            Properties.AppSettings.Default.Save();

            ToastNotificationsHelper.
            SendNotification(
            App.LocalizationService[AppLocalization.Sucsess],
            App.LocalizationService[AppLocalization.SavedSucsessfully],
            Notification.Wpf.NotificationType.Success);
        }
        private async Task<bool> LoadProfile()
        {
            try
            {                    

                this.hoursTxt.Text = Properties.AppSettings.Default.TimeRemainder.Hours.ToString();
                this.minutesTxt.Text = Properties.AppSettings.Default.TimeRemainder.Minutes.ToString();
                this.countryBox.SelectedValue = Properties.AppSettings.Default.country;
                this.cityBox.SelectedValue = Properties.AppSettings.Default.city;
                this.calcBox.SelectedValue = Properties.AppSettings.Default.method;
                this.calcBox.ToolTip = App.LocalizationService[AppLocalization.calculationMethodToolTip];

                this.arabicRadioBtn.IsChecked = Properties.AppSettings.Default.Lang == AppLocalization.Arabic;
                this.englishRadioBtn.IsChecked = Properties.AppSettings.Default.Lang == AppLocalization.English;
                this.frenshRadioBtn.IsChecked = Properties.AppSettings.Default.Lang == AppLocalization.Frensh;

                this.startupTogle.IsChecked = Properties.AppSettings.Default.AutoStartUp;
                this.adhanBox.SelectedValue = Properties.AppSettings.Default.Adhan;
                this.adhanFajrBox.SelectedValue = Properties.AppSettings.Default.AdhanFajr;
                this.notificationToggle.IsChecked = Properties.AppSettings.Default.NotificationEnabled;
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        private void notificationToggle_Checked(object sender, RoutedEventArgs e)
        {

        }
        private async void show_Click(object sender, RoutedEventArgs e)
        {
           //// Get the parent MetroWindow and cast it
           // var metroWindow = Application.Current.MainWindow as MetroWindow;
           // if (metroWindow != null)
           // {
           //     await ShowPopupAsync(metroWindow);
           // }
           // else
           // {
           //     MessageBox.Show("The main window is not a MetroWindow.");
           // }
        }
    }
}
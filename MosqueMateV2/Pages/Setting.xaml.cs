using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Extensions;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Reflection;
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
        bool IsPLaying { get; set; }
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
            this.Title = App.LocalizationService[SD.Localization.Settings];
            this.reminderTimeLBL.Content = App.LocalizationService[SD.Localization.ReminderTime];
            this.hoursLBL.Content = App.LocalizationService[SD.Localization.Hours];
            this.minutesLBL.Content = App.LocalizationService[SD.Localization.Minutes];
            this.minutesLBL.Content = App.LocalizationService[SD.Localization.Minutes];
            this.countryLBL.Content = App.LocalizationService[SD.Localization.countryLBL];
            this.cityLBL.Content = App.LocalizationService[SD.Localization.cityLBL];
            this.regionLBL.Content = App.LocalizationService[SD.Localization.regionLBL];
            this.LanguageLBL.Content = App.LocalizationService[SD.Localization.Language];
            this.calcLBL.Content = App.LocalizationService[SD.Localization.calculationMethod];
            this.calcLBL.Content = App.LocalizationService[SD.Localization.calculationMethod];
            this.startupToggleLBL.Content = App.LocalizationService[SD.Localization.autoStartUp];
            this.adhanLBL.Content = App.LocalizationService[SD.Localization.Muezzin];
            this.adhanFajrLBL.Content = App.LocalizationService[SD.Localization.adhanFajr];
            this.notificationLBL.Content = App.LocalizationService[SD.Localization.notificationLBL];
            this.reciterLBL.Content = App.LocalizationService[SD.Localization.Reciter];
            this.themeModeLBL.Content = App.LocalizationService[SD.Localization.ChooseMode];
            this.save.Content = App.LocalizationService[SD.Localization.Save];
            this.notificationToggle.ToolTip = App.LocalizationService[SD.Localization.notificationDesc];

            #endregion

            this.countryBox.IsEnabled = false;
            this.adhanBox.IsEnabled = false;
            this.adhanFajrBox.IsEnabled = false;

            #region reciterBoxItems
            var receiters = EnumHelper<ReciterTypesEnum>.ConvertEnumToFormattedList();
            reciterBox.ItemsSource = receiters;
            #endregion

            #region themeBoxItems
            var theme = ReflectionExtensions.GetPropertyNames<ThemeMode>(
                BindingFlags.Public,
                BindingFlags.Static);
            theme.Remove(nameof(ThemeMode.None));
            theme.Remove(nameof(ThemeMode.System));
            themeModeBox.ItemsSource = theme;
            #endregion

            #region BackgroundWorker-Area
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: token => jsonCountry.GetAllCountiresAsync(),
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
                         backgroundTask: token => resourceManager.GetAllResourcesInfoFromResxAsync(),
                         onSuccess: result =>
                         {
                             this.adhanBox.IsEnabled = true;
                             this.adhanFajrBox.IsEnabled = true;
                             this.adhanBox.ItemsSource = result.
                                                          Select(x => x.Name).
                                                          Where(x => !x.Contains(PrayerEnum.Fajr.ToString()) && !x.Contains("Now") && !x.Contains("pick"))
                                                         .AddSpacesBetweenWords().OrderBy(x => x);
                             this.adhanFajrBox.ItemsSource = result.
                                                          Select(x => x.Name).
                                                          Where(x => x.Contains(PrayerEnum.Fajr.ToString()) && !x.Contains("Now") && !x.Contains("pick"))
                                                         .AddSpacesBetweenWords().OrderBy(x => x);
                         },
                         retryNumber: 2,
                         () => // handle an error
                         {

                         });
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: token => LoadProfile(),
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


            string lang = arabicRadioBtn.IsChecked == true ? SD.Localization.Arabic :
                          englishRadioBtn.IsChecked == true ? SD.Localization.English :
                          frenshRadioBtn.IsChecked == true ? SD.Localization.Frensh :
                          SD.Localization.Arabic;
            #endregion

            #region AutoStart
            var autoStart = startupTogle.IsChecked;
            var notification = notificationToggle.IsChecked;
            #endregion

            #region TheMuzzein
            var adhan = adhanBox.SelectedValue as string;
            var adhanFajr = adhanFajrBox.SelectedValue as string;

            #endregion

            #region Reciter
            var reciter = reciterBox.SelectedValue as string;

            #endregion

            #region Theme-Mode
            var theme = themeModeBox.SelectedValue as string;

            #endregion

            #region Set-Value-Settings
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Lang)] = lang;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.TimeRemainder)] = remainderTime;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Country)] = country;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.City)] = city;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Method)] = calc;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.AutoStartUp)] = autoStart;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Adhan)] = adhan;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.AdhanFajr)] = adhanFajr;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.NotificationEnabled)] = notification;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Reciter)] = reciter;
            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.themeMode)] = theme;
            #endregion


            Properties.AppSettings.Default.Save();

            ToastNotificationsHelper.
            SendNotification(
                        title: App.LocalizationService[SD.Localization.Sucsess],
                        message: App.LocalizationService[SD.Localization.SavedSucsessfully],
                        duration: new TimeSpan(0, 0, SD.Localization.NotificatonDuration),
                        onClose: () =>
                        {
                            AppHelper.RestartApp();
                        },
           type: Notification.Wpf.NotificationType.Success);

        }
        private async Task<bool> LoadProfile()
        {
            try
            {

                this.hoursTxt.Text = Properties.AppSettings.Default.TimeRemainder.Hours.ToString();
                this.minutesTxt.Text = Properties.AppSettings.Default.TimeRemainder.Minutes.ToString();
                this.countryBox.SelectedValue = Properties.AppSettings.Default.Country;
                this.cityBox.SelectedValue = Properties.AppSettings.Default.City;
                this.calcBox.SelectedValue = Properties.AppSettings.Default.Method;
                this.reciterBox.SelectedValue = Properties.AppSettings.Default.Reciter;
                this.themeModeBox.SelectedValue = Properties.AppSettings.Default.themeMode;
                this.calcBox.ToolTip = App.LocalizationService[SD.Localization.calculationMethodToolTip];

                this.arabicRadioBtn.IsChecked = Properties.AppSettings.Default.Lang == SD.Localization.Arabic;
                this.englishRadioBtn.IsChecked = Properties.AppSettings.Default.Lang == SD.Localization.English;
                this.frenshRadioBtn.IsChecked = Properties.AppSettings.Default.Lang == SD.Localization.Frensh;

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

        private void adhanBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsPLaying)
            {
                if (adhanBox.SelectedValue is not null)
                {
                    var value = adhanBox?.SelectedValue as string;
                    if (value is not null)
                    {
                        var res = resourceManager.GetResourceByte(value.Replace(" ", ""));
                        if (res is not null)
                        {
                            App.mP3Player.Play(res);
                        }
                    }
                }
            }
            else
                IsPLaying = true;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            App.mP3Player.Stop();
        }
    }
}
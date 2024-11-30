using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Extensions;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
        private void FillAdhan()
        {

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
            string lang = arabicRadioBtn.IsChecked == true ? AppLocalization.Arabic :
                          englishRadioBtn.IsChecked == true ? AppLocalization.English :
                          frenshRadioBtn.IsChecked == true ? AppLocalization.Frensh :
                          AppLocalization.Arabic;

            Properties.AppSettings.Default[nameof(Properties.AppSettings.Default.Lang)] = lang;     
            Properties.AppSettings.Default.Save();
            MessageBox.Show("saved");
        }
    }
}
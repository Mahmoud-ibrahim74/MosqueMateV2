using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Controls;
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
        RxTaskManger rxTaskManger;
        public Setting()
        {
            InitializeComponent();
            rxTaskManger = new();
            jsonCountry = new JsonCountryRepository();
            jsonCity = new JsonCityRepository();
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
            #endregion

            this.countryBox.IsEnabled = false;
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: () => jsonCountry.GetAllCountires(),
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
            if(data is not null && data.Any())
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
    }
}
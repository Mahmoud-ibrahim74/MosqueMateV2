using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
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
        RxTaskManger rxTaskManger;
        public Setting()
        {
            InitializeComponent();
            rxTaskManger = new();  
            jsonCountry = new JsonCountryRepository();
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
            #endregion

            this.countryBox.IsEnabled = false;
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: () => jsonCountry.GetAllCountiresLocalization(),
                 onSuccess: result =>
                 {
                     this.countryBox.IsEnabled = true;
                     this.countryBox.ItemsSource = result;
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

        private void countryBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }
    }
}
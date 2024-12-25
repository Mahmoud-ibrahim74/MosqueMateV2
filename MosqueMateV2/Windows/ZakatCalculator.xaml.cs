using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class ZakatCalculator : Window
    {
        double zakatValue { get; set; }
        DTOQuestions currentQuestion { get; set; }
        public ZakatCalculator()
        {
            #region SetTheme
            var currentTheme = AppSettings.Default.themeMode.ToThemeMode();
            this.ThemeMode = currentTheme;
            #endregion
            InitializeComponent();

        }
        public void ShowModal()
        {

            this.Visibility = Visibility.Visible;  // Ensure the modal is visible
            this.Opacity = 0;  // Start as invisible

            // Apply the animation
            var popupAnimation = (Storyboard)Resources["PopupAnimation"];
            popupAnimation.Begin(this);
            this.ShowDialog();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppHelper.GoHome();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.zakatTitleLBL.FlowDirection = App.AppLanguage == SD.Localization.Arabic ?
                FlowDirection.RightToLeft :
                FlowDirection.LeftToRight;     
            this.zkatMonyTxt.FlowDirection = App.AppLanguage == SD.Localization.Arabic ?
                FlowDirection.RightToLeft :
                FlowDirection.LeftToRight;

            this.Title = App.LocalizationService[SD.Localization.Zakat];
            this.zakatTitleTxt.Text = App.LocalizationService[SD.Localization.ZakatTitle];
            this.zakatDescritpionTxt.Text = App.LocalizationService[SD.Localization.zakatDescritpion];
            this.zakatTitleLBL.Content = App.LocalizationService[SD.Localization.ZakatAmount];
            this.zakatTitleResultLBL.Text = App.LocalizationService[SD.Localization.ZakatValue] + " : " + zakatValue;
        }

        private void zkatMonyTxt_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !NumberHelper.IsTextNumeric(e.Text);
        }

        private void zkatMonyTxt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(zkatMonyTxt.Text))
            {
                zakatValue = 0;
                this.zakatTitleResultLBL.Text = App.LocalizationService[SD.Localization.ZakatValue] + " : " + zakatValue;
            }
            if (!string.IsNullOrWhiteSpace(zkatMonyTxt.Text))
            {
                var value = long.Parse(zkatMonyTxt.Text.Trim());
                zakatValue = (value) * (2.5 / 100);
                zakatValue = Math.Round(zakatValue, 2);
                this.zakatTitleResultLBL.Text = App.LocalizationService[SD.Localization.ZakatValue] + " : " + zakatValue;
            }
        }
    }
}

using ModernWpf.Controls;
using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Helpers;
using MosqueMateV2.Pages;
using MosqueMateV2.Resources;
using System.Windows;

namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string apiContent { get; set; } = string.Empty;
        public List<PrayerSlide> PrayerSlidesData { get; set; }
        RxTaskManger rxTaskManger;
        public MainWindow()
        {
            InitializeComponent();
            rxTaskManger = new();
            PrayerSlidesData = [];
        }

        private void NavView_SelectionChanged(object sender, NavigationViewSelectionChangedEventArgs e)
        {
            if (e.SelectedItem is NavigationViewItem selectedItem)
            {
                string pageTag = selectedItem.Tag?.ToString();
                if (e.IsSettingsSelected) // Check if the Settings button is selected
                {
                    MainFrame.Navigate(new Setting());
                    return;
                }

                // Navigate to the appropriate page
                switch (pageTag)
                {
                    case AppLocalization.HomeTag:
                        MainFrame.Navigate(new Home());
                        break;
                    case AppLocalization.QuranTag:
                        MainFrame.Navigate(new Quran());
                        break;
                    case AppLocalization.AdhkarTag:
                        MainFrame.Navigate(new Adhkar());
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Title = App.LocalizationService[AppLocalization.AppName];
            NavView.PaneTitle = App.LocalizationService[AppLocalization.MainMenu];
            homeItem.Content = App.LocalizationService[AppLocalization.Home];
            quraanItem.Content = App.LocalizationService[AppLocalization.Quran];
            adhkarItem.Content = App.LocalizationService[AppLocalization.AzkarDoaa];
            homeItem.Tag = AppLocalization.HomeTag;
            quraanItem.Tag = AppLocalization.QuranTag;
            adhkarItem.Tag = AppLocalization.AdhkarTag;
            NavView.SelectedItem = homeItem;
            MainFrame.Navigate(new Home());
        }
    }
}
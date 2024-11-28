using ModernWpf.Controls;
using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Pages;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using Resources;
using System.Windows;
using System.Windows.Controls;
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
        RxTaskManger rxTaskManger;
        public MainWindow()
        {
            InitializeComponent();
            rxTaskManger = new();
            ApiClient.Configure("cairo", "egypt", 8);
            PrayerSlidesData = [];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = App.LocalizationService[AppLocalization.AppName];
            NavView.PaneTitle = App.LocalizationService[AppLocalization.MainMenu];
            homeItem.Content = App.LocalizationService[AppLocalization.Home];
            homeItem.Tag = AppLocalization.HomeTag;
            NavView.SelectedItem = NavView.SettingsItem;
            MainFrame.Navigate(new Setting());

        }
        public void BindNavMenu()
        {
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
                    case "GradesPage":
                        Console.WriteLine("GradesPage");
                        //MainFrame.Navigate(new GradesPage());
                        break;
                    case "TutorsPage":
                        Console.WriteLine("TutorsPage");
                        //MainFrame.Navigate(new TutorsPage());
                        break;
                }
            }
        }
    }
}
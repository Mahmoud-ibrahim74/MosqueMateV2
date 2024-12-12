using ModernWpf.Controls;
using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Pages;
using MosqueMateV2.Resources;
using System.Drawing;
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
        BitmapIcon _quranIcon { get; set; }
        BitmapIcon _beadsIcon { get; set; }
        BitmapIcon _readQuran { get; set; }
        BitmapIcon allah { get; set; }
        BitmapIcon Prophet { get; set; }
        BitmapIcon Rokay { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PrayerSlidesData = [];

            #region SideBarIcons
            _quranIcon = new();
            _beadsIcon = new();
            _readQuran = new();
            Prophet = new();
            allah = new();
            Rokay = new();
            _quranIcon.UriSource = new Uri("pack://application:,,,/Assets/quranSideBar.png");
            _beadsIcon.UriSource = new Uri("pack://application:,,,/Assets/beads.png");
            _readQuran.UriSource = new Uri("pack://application:,,,/Assets/read-quran.png");
            allah.UriSource = new Uri("pack://application:,,,/Assets/allah.png");
            allah.UriSource = new Uri("pack://application:,,,/Assets/allah.png");
            Prophet.UriSource = new Uri("pack://application:,,,/Assets/muhammad.png");
            Rokay.UriSource = new Uri("pack://application:,,,/Assets/dua.png"); 

            quraanItem.Icon = _quranIcon;
            adhkarItem.Icon = _beadsIcon;
            hadithItem.Icon = _readQuran;
            allahNamesItem.Icon = allah;
            ProphetsStoriesItem.Icon = Prophet;
            RokyaItems.Icon = Rokay;

            #endregion
        }

        private void NavView_SelectionChanged(object sender, NavigationViewSelectionChangedEventArgs e)
        {
            if (e.SelectedItem is NavigationViewItem selectedItem)
            {
                string pageTag = selectedItem.Tag?.ToString();
                if (e.IsSettingsSelected)
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
                    case AppLocalization.Hadith:
                        MainFrame.Navigate(new Hadith());
                        break;
                    case AppLocalization.allahName:
                        MainFrame.Navigate(new AllahNames());
                        break;
                    case AppLocalization.Prophets:
                        MainFrame.Navigate(new PdfViewerPage(Domain.Enums.ViewTypesEnum.Stories));
                        break;           
                    case AppLocalization.Rokya:
                        MainFrame.Navigate(new PdfViewerPage(Domain.Enums.ViewTypesEnum.Rokya));
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            #region Labels
            Title = App.LocalizationService[AppLocalization.AppName];
            NavView.PaneTitle = App.LocalizationService[AppLocalization.MainMenu];
            homeItem.Content = App.LocalizationService[AppLocalization.Home];
            quraanItem.Content = App.LocalizationService[AppLocalization.Quran];
            adhkarItem.Content = App.LocalizationService[AppLocalization.AzkarDoaa];
            hadithItem.Content = App.LocalizationService[AppLocalization.Hadith];
            allahNamesItem.Content = App.LocalizationService[AppLocalization.allahName];
            ProphetsStoriesItem.Content = App.LocalizationService[AppLocalization.Prophets];
            RokyaItems.Content = App.LocalizationService[AppLocalization.Rokya];
            #endregion

            #region Tags

            homeItem.Tag = AppLocalization.HomeTag;
            quraanItem.Tag = AppLocalization.QuranTag;
            adhkarItem.Tag = AppLocalization.AdhkarTag;
            hadithItem.Tag = AppLocalization.Hadith;
            allahNamesItem.Tag = AppLocalization.allahName;
            ProphetsStoriesItem.Tag = AppLocalization.Prophets;
            RokyaItems.Tag = AppLocalization.Rokya;
            NavView.SelectedItem = homeItem;
            #endregion

            MainFrame.Navigate(new Home());
        }
    }
}
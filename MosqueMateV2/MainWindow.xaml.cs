using AngleSharp.Common;
using ModernWpf;
using ModernWpf.Controls;
using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Pages;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using MosqueMateV2.Windows;
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
        BitmapIcon _quranTafseerIcon { get; set; }
        BitmapIcon _beadsIcon { get; set; }
        BitmapIcon _readQuran { get; set; }
        BitmapIcon allah { get; set; }
        BitmapIcon Prophet { get; set; }
        BitmapIcon Rokay { get; set; }
        BitmapIcon Crescent { get; set; }
        BitmapIcon Kodosy { get; set; }
        BitmapIcon Wasay { get; set; }
        BitmapIcon Radio { get; set; }
        BitmapIcon Calculator { get; set; }

        public MainWindow()
        {
            #region SetTheme
            var currentTheme = AppSettings.Default.themeMode.ToThemeMode();
            this.ThemeMode = currentTheme;
            #endregion

            InitializeComponent();
            PrayerSlidesData = [];

            #region SideBarIcons
            _quranIcon = new();
            _beadsIcon = new();
            _readQuran = new();
            Prophet = new();
            allah = new();
            Rokay = new();
            Crescent = new();
            Kodosy = new();
            Wasay = new();
            Radio = new();
            _quranTafseerIcon = new();
            Calculator = new();
            _quranIcon.UriSource = new Uri("pack://application:,,,/Assets/quranSideBar.png");
            _quranTafseerIcon.UriSource = new Uri("pack://application:,,,/Assets/quranSideBar.png");
            _beadsIcon.UriSource = new Uri("pack://application:,,,/Assets/beads.png");
            _readQuran.UriSource = new Uri("pack://application:,,,/Assets/read-quran.png");
            allah.UriSource = new Uri("pack://application:,,,/Assets/allah.png");
            allah.UriSource = new Uri("pack://application:,,,/Assets/allah.png");
            Prophet.UriSource = new Uri("pack://application:,,,/Assets/muhammad.png");
            Rokay.UriSource = new Uri("pack://application:,,,/Assets/dua.png");
            Crescent.UriSource = new Uri("pack://application:,,,/Assets/crescent.png");
            Kodosy.UriSource = new Uri("pack://application:,,,/Assets/qudsi.png");
            Wasay.UriSource = new Uri("pack://application:,,,/Assets/wsaya.png");
            Radio.UriSource = new Uri("pack://application:,,,/Assets/signal.png");
            Calculator.UriSource = new Uri("pack://application:,,,/Assets/calculator.png");

            quraanItem.Icon = _quranIcon;
            quraanTafseerItem.Icon = _quranTafseerIcon;
            adhkarItem.Icon = _beadsIcon;
            hadithItem.Icon = _readQuran;
            allahNamesItem.Icon = allah;
            ProphetsStoriesItem.Icon = Prophet;
            RokyaItems.Icon = Rokay;
            CrescentItems.Icon = Crescent;
            KosdosyItems.Icon = Kodosy;
            WasayaItems.Icon = Wasay;
            RadioItems.Icon = Radio;
            CalculatorItems.Icon = Calculator;
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

                switch (pageTag)
                {
                    case SD.Localization.HomeTag:
                        MainFrame.Navigate(new Home());
                        break;
                    case SD.Localization.QuranTag:
                        MainFrame.Navigate(new Quran());
                        break;
                    case SD.Localization.AdhkarTag:
                        MainFrame.Navigate(new Adhkar());
                        break;
                    case SD.Localization.Hadith:
                        MainFrame.Navigate(new Hadith());
                        break;
                    case SD.Localization.allahName:
                        MainFrame.Navigate(new AllahNames());
                        break;
                    case SD.Localization.Prophets:
                        MainFrame.Navigate(new ProphertStories());
                        break;
                    case SD.Localization.MiniQuranLive:
                        if (!AppHelper.IsWindowOpen<LiveStream>("LiveStreamWindow", true))
                            new LiveStream(SD.Localization.LiveStreamUrl).Show();
                        break;
                    case SD.Localization.Rokya:
                        MainFrame.Navigate(new PdfViewerPage(Domain.Enums.ViewTypesEnum.Rokya));
                        break;
                    case SD.Localization.FortyNawawi:
                        MainFrame.Navigate(new PdfViewerPage(Domain.Enums.ViewTypesEnum.FortyNawawi));
                        break;
                    case SD.Localization.Kodosy:
                        MainFrame.Navigate(new PdfViewerPage(Domain.Enums.ViewTypesEnum.Kodosy));
                        break;
                    case SD.Localization.Wasaya:
                        MainFrame.Navigate(new PdfViewerPage(Domain.Enums.ViewTypesEnum.Wasaya));
                        break;
                    case SD.Localization.QuranTafseer:
                        MainFrame.Navigate(new QuranTafseer());
                        break;
                    case SD.Localization.Zakat:
                        if (!AppHelper.IsWindowOpen<ZakatCalculator>("zakatCalculatorWindow", true))
                            new ZakatCalculator().ShowModal();
                        break;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            #region Labels
            Title = App.LocalizationService[SD.Localization.AppName];
            NavView.PaneTitle = App.LocalizationService[SD.Localization.MainMenu];
            homeItem.Content = App.LocalizationService[SD.Localization.Home];
            quraanItem.Content = App.LocalizationService[SD.Localization.Quran];
            adhkarItem.Content = App.LocalizationService[SD.Localization.AzkarDoaa];
            hadithItem.Content = App.LocalizationService[SD.Localization.Hadith];
            allahNamesItem.Content = App.LocalizationService[SD.Localization.allahName];
            ProphetsStoriesItem.Content = App.LocalizationService[SD.Localization.Prophets];
            RokyaItems.Content = App.LocalizationService[SD.Localization.Rokya];
            quraanTafseerItem.Content = App.LocalizationService[SD.Localization.QuranTafseer];
            quraanTafseerItem.Content = App.LocalizationService[SD.Localization.QuranTafseer];
            CrescentItems.Content = App.LocalizationService[SD.Localization.FortyNawawi];
            KosdosyItems.Content = App.LocalizationService[SD.Localization.Kodosy];
            WasayaItems.Content = App.LocalizationService[SD.Localization.Wasaya];
            RadioItems.Content = App.LocalizationService[SD.Localization.MiniQuranLive];
            CalculatorItems.Content = App.LocalizationService[SD.Localization.Zakat];
            #endregion

            #region Tags

            homeItem.Tag = SD.Localization.HomeTag;
            quraanItem.Tag = SD.Localization.QuranTag;
            adhkarItem.Tag = SD.Localization.AdhkarTag;
            hadithItem.Tag = SD.Localization.Hadith;
            allahNamesItem.Tag = SD.Localization.allahName;
            ProphetsStoriesItem.Tag = SD.Localization.Prophets;
            RokyaItems.Tag = SD.Localization.Rokya;
            quraanTafseerItem.Tag = SD.Localization.QuranTafseer;
            CrescentItems.Tag = SD.Localization.FortyNawawi;
            KosdosyItems.Tag = SD.Localization.Kodosy;
            WasayaItems.Tag = SD.Localization.Wasaya;
            RadioItems.Tag = SD.Localization.MiniQuranLive;
            CalculatorItems.Tag = SD.Localization.Zakat;
            NavView.SelectedItem = homeItem;

            #endregion

            MainFrame.Navigate(new Home());
        }
    }
}
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using MosqueMateV2.Service.IServices;
using MosqueMateV2.Service.Services;
using System.Windows;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PrayerLearning : Page
    {

        RxTaskManger rxTaskManger;
        public IYoutubeService _youtubeService;

        public PrayerLearning()
        {
            InitializeComponent();
            rxTaskManger = new();
            _youtubeService = new YoutubeService();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.loader.Visibility = Visibility.Visible;
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => _youtubeService.GetPlayListAsync(SD.Localization.PrayerLerningUrl),
                 onSuccess: result =>
                 {
                     var list = result.ToList();
                     GridCardContainer.GenerateCards(
                                   data: list,
                                   getName: item => item.Title,
                                   getId: item => new Random().Next(1, 1000000),
                                   UidCard: item => item.Url,
                                   PaddingTopTxt: 30,
                                   CardWidth: 380,
                                   CardHeight: 150,
                                   FontSizeHeader: 10,
                                   FontSizeText: 15,
                                   textWrapping: TextWrapping.Wrap,
                                   serviceType: PagesTypesEnum.YoutubeViewerPage
                               );
                     this.loader.Visibility = Visibility.Collapsed;

                 },
                 retryNumber: 2,
                 () => // handle an error
                 {
                     this.loader.Visibility = Visibility.Collapsed;

                 });
        }

        private void GridCardContainer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyboardHelper.ActionPressCTRLKey(e, System.Windows.Input.Key.B, AppHelper.GoHome);
        }
    }
}
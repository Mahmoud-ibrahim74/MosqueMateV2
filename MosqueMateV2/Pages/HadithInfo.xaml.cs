using HandyControl.Controls;
using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Input;
using Page = ModernWpf.Controls.Page;
using ScrollViewer = System.Windows.Controls.ScrollViewer;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HadithInfo : Page
    {

        RxTaskManger rxTaskManger;
        public ISuraRepository _suraRepository;
        DTOHadithInfo hadithInfo { get; set; }
        int _chapterNumber { get; set; }
        int _pageSize { get; set; } = 200;
        private LoadingLine _Loading { get; set; }

        public HadithInfo(int chapterNumber)
        {
            InitializeComponent();
            rxTaskManger = new();
            _suraRepository = new SuraRepository();
            this._chapterNumber = chapterNumber;
            HadithHelper.SelectedChapter = chapterNumber;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SendRequestAsync();
        }

        private void SendRequestAsync()
        {
            this.loader.Visibility = Visibility.Visible;
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => new ApiClient(_baseUrl: ApiRquestHelper.HadithCollectionLink(
                     bookSlug: HadithHelper.SelectedBook,
                     chapterNumber: this._chapterNumber,
                     paginate: _pageSize
                     )).GetAsync(),
                 onSuccess: result =>
                 {
                     if (result is not null)
                     {
                         hadithInfo = JsonConvert.DeserializeObject<DTOHadithInfo>(result);
                         if (hadithInfo?.status == 200)
                         {
                             GridCardContainer.GenerateCardsForHadith(hadithInfo.hadiths.data);
                             this.loader.Visibility = Visibility.Collapsed;
                             GridCardContainer.Focus(); 
                         }
                     }
                 },

                 retryNumber: 2,
                 () => // handle an error
                 {
                     this.loader.Visibility = Visibility.Collapsed;
                 });
        }

        private void quranScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {
                    //_pageSize = 60;
                    //GridCardContainer.Children.Clear();
                    //_Loading = ControlExtenstion.GenerateLineLoading();
                    //_Loading.Visibility = Visibility.Visible;
                    //GridCardContainer.Children.Add(_Loading);
                    //SendRequestAsync();
                }
            }
        }

        private void GotoTop_Click(object sender, RoutedEventArgs e)
        {
            quranScrollViewer.ScrollToTop();
        }

        private void GridCardContainer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyboardHelper.ActionPressCTRLKey(e, Key.B, () =>
            {
                AppHelper.GoBack();
            });
        }
    }
}
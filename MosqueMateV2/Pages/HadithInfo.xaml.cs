using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Windows;
using Page = ModernWpf.Controls.Page;

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
        int _chapterNumber {  get; set; }
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
            SendRequestAsync(25);
        }

        private void SendRequestAsync(int page)
        {
            this.loader.Visibility = Visibility.Visible;

            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => new ApiClient(_baseUrl: ApiRquestHelper.HadithCollectionLink(
                     bookSlug: HadithHelper.SelectedBook,
                     chapterNumber: this._chapterNumber,
                     paginate:page
                     )).GetAsync(),
                 onSuccess: result =>
                 {
                     hadithInfo = JsonConvert.DeserializeObject<DTOHadithInfo>(result);
                     if (hadithInfo?.status == 200)
                     {
                         GridCardContainer.GenerateCardsForHadith(hadithInfo.hadiths.data);
                         this.loader.Visibility = Visibility.Collapsed;
                         Console.WriteLine("GridCardContainer.Children  " + GridCardContainer.Children.Count);
                     }
                 },
                 retryNumber: 2,
                 () => // handle an error
                 {

                 });
        }
    }
}
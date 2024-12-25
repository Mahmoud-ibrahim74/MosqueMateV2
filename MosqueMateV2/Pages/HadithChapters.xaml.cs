using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Input;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HadithChapters : Page
    {

        RxTaskManger rxTaskManger;
        public ISuraRepository _suraRepository;
        DTOChapter chapter { get; set; }
        string _bookSlug {  get; set; }
        public HadithChapters(string bookSlug)
        {
            InitializeComponent();
            rxTaskManger = new();
            _suraRepository = new SuraRepository();
            this._bookSlug = bookSlug;  
            HadithHelper.SelectedBook = bookSlug;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow;
            this.loader.Visibility = Visibility.Visible;  
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => new ApiClient(_baseUrl:ApiRquestHelper.HadithChpterLink(this._bookSlug)).GetAsync(),
                 onSuccess: result =>
                 {
                     if (result is not null)
                     {
                         chapter = JsonConvert.DeserializeObject<DTOChapter>(result);
                         if (chapter?.status == 200)
                         {
                             GridCardContainer.GenerateCards(
                                                   data: chapter.chapters,
                                                   getName: item =>"(" + item.chapterNumber + ")" + "\n" + 
                                                   item.chapterArabic + "\n" + 
                                                   item.chapterEnglish,
                                                   getId: item => int.Parse(item.chapterNumber),
                                                   serviceType: PagesTypesEnum.HadithChapter,
                                                   PaddingTopTxt: 10,
                                                   CardWidth: 380,
                                                   CardHeight: 150,
                                                   FontSizeHeader: 10,
                                                   FontSizeText:18,
                                                   textWrapping: TextWrapping.Wrap
                                               );
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

        private void GridCardContainer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyboardHelper.ActionPressCTRLKey(e, Key.B, () =>
            {
                AppHelper.GoBack();
            });
        }

        private void GotoTop_Click(object sender, RoutedEventArgs e)
        {
            hadithScrollViewer.ScrollToTop();
        }
    }
}
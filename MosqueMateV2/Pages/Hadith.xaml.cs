using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
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
    public partial class Hadith : Page
    {

        RxTaskManger rxTaskManger;
        public ISuraRepository _suraRepository;
        DTOBookResponse hadithBooks { get; set; }

        public Hadith()
        {
            InitializeComponent();
            rxTaskManger = new();
            _suraRepository = new SuraRepository();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow;
            this.loader.Visibility = Visibility.Visible;  
            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => new ApiClient(_baseUrl:AppLocalization.HadithApiLink).GetAsync(),
                 onSuccess: result =>
                 {
                     hadithBooks = JsonConvert.DeserializeObject<DTOBookResponse>(result);
                     if (hadithBooks?.status == 200)
                     {
                         GridCardContainer.GenerateMaterialDesignCards(
                                               data: hadithBooks.books,
                                               getName: item => item.bookName,
                                               getId: item => item.id,
                                               serviceType: ServiceTypeEnum.Hadith,
                                               CardWidth:350,
                                               CardHeight:100
                                           );
                         this.loader.Visibility = Visibility.Collapsed;

                     }
                 },
                 retryNumber: 2,
                 () => // handle an error
                 {

                 });
        }
        private void searchOnQuran_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AdhkarSearchTxt.Text))
            {
                var offest = GridCardContainer.GetOffsetChildOfElement(AdhkarSearchTxt.Text);
                if (offest.ControlPosition is not null)
                {
                    quranScrollViewer.ScrollToVerticalOffset(offest.ControlPosition.Value.Y - 300);
                    if (offest.SelectedElement is not null)
                    {
                        AnimationHelper animation = new(AppLocalization.AnimationDuration);
                        animation.CreatePulseAnimation(offest.SelectedElement);
                        animation.StartPulseAnimation();
                    }
                }
            }
        }
    }
}
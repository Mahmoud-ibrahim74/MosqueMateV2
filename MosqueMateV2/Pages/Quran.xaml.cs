using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Quran : Page
    {

        RxTaskManger rxTaskManger;
        public ISuraRepository  _suraRepository;

        public Quran()
        {
            InitializeComponent();
            rxTaskManger = new();
            _suraRepository = new SuraRepository();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow;

            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => _suraRepository.GetAllSuraNames(),
                 onSuccess: result =>
                 {
                     GridCardContainer.GenerateCards(
                                   data: result,
                                   getName: item => item.name,
                                   getId: item => item.pageIndex,
                                   PaddingTopTxt: 50,
                                   CardWidth: 380,
                                   CardHeight: 150,
                                   FontSizeHeader: 10, 
                                   FontSizeText: 25,
                                   textWrapping: TextWrapping.Wrap,
                                   serviceType: PagesTypesEnum.Quran
                               );
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
                        AnimationHelper animation = new(SD.Localization.AnimationDuration);
                        animation.CreatePulseAnimation(offest.SelectedElement);
                        animation.StartPulseAnimation();
                    }
                }
            }
        }

        private void GotoTop_Click(object sender, RoutedEventArgs e)
        {
            quranScrollViewer.ScrollToTop();
        }
    }
}
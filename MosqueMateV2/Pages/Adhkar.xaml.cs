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
    public partial class Adhkar : Page
    {

        RxTaskManger rxTaskManger;
        public IJsonAdhkarRepository jsonAdhkar;

        public Adhkar()
        {
            InitializeComponent();
            rxTaskManger = new();
            jsonAdhkar = new JsonAdhkarRepository();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow;

            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => jsonAdhkar.GetAllAdhkarsAsync(),
                 onSuccess: result =>
                 {
                     GridCardContainer.GenerateCards(
                                   data: result,
                                   getName: item => item.category,
                                   getId: item =>item.id,
                                   PaddingTopTxt: 40,
                                   CardWidth: 380,
                                   CardHeight: 150,
                                   FontSizeHeader: 10,
                                   FontSizeText: 20,
                                   textWrapping: TextWrapping.Wrap,
                                   serviceType: PagesTypesEnum.Adhkar
                                );
                     GridCardContainer.Focus();
                 },
                 retryNumber: 2,
                 () => // handle an error
                 {

                 });
        }
        private void searchOnAdhan_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AdhkarSearchTxt.Text))
            {
                var offest = GridCardContainer.GetOffsetChildOfElement(AdhkarSearchTxt.Text);
                if (offest.ControlPosition is not null)
                {
                    adhkarScrollViewer.ScrollToVerticalOffset(offest.ControlPosition.Value.Y - 300);
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
            adhkarScrollViewer.ScrollToTop();
        }

        private void GridCardContainer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyboardHelper.ActionPressCTRLKey(
                e,
                System.Windows.Input.Key.B,
                 AppHelper.GoHome
                );
        }
    }
}
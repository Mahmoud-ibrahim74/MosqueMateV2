using MaterialDesignThemes.Wpf;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using System.Windows;
using System.Windows.Controls;
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
                 backgroundTask: () => jsonAdhkar.GetAllAdhkarsAsync(),
                 onSuccess: result =>
                 {
                     GridCardContainer.GenerateMaterialDesignCards(result);
                 },
                 retryNumber: 2,
                 () => // handle an error
                 {

                 });
        }

        private void AdhkarSearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AdhkarSearchTxt.Text))
            {
                var offest = GridCardContainer.GetOffsetChildOfElement(AdhkarSearchTxt.Text);
                if (offest.ControlPosition is not null)
                {
                    adhkarScrollViewer.ScrollToVerticalOffset(offest.ControlPosition.Value.Y - 300);
                    if (offest.SelectedElement is not null)
                        ControlHelper.PulseCard(offest.SelectedElement);
                }
            }
        }
    }
}
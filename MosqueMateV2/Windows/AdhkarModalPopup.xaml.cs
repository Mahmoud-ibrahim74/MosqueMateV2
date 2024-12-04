using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using System.Windows;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class AdhkarModalPopup : Window
    {
        int zekr_id { get; set; }
        DTOAdhkar _zekr { get; set; }
        private int zekrCategoryCount;
        private int ZekrIndex = 1;
        private int ZekrIndexCount = 1;
        RxTaskManger rxTaskManger;
        IJsonAdhkarRepository jsonAdhkar;

        public AdhkarModalPopup(int zekId)
        {
            InitializeComponent();
            rxTaskManger = new();
            this.zekr_id = zekId;
            jsonAdhkar = new JsonAdhkarRepository();
        }
        private void NextZekr()
        {
            if (ZekrIndex == _zekr.zekrContent.Count)
                return;
            ZekrIndex++;
            zekrDescription.Text = _zekr.zekrContent[ZekrIndex].text;
            ZekrIndexCount = _zekr.zekrContent.Count;


        }
        private void PrevZekr()
        {
            if (ZekrIndex == 1)
                return;
            ZekrIndex--;
            zekrDescription.Text = _zekr.zekrContent[ZekrIndex].text;
            ZekrIndexCount = _zekr.zekrContent.Count;
        }
        // Show the modal with popup animation
        public void ShowModal()
        {

            this.Visibility = Visibility.Visible;  // Ensure the modal is visible
            this.Opacity = 0;  // Start as invisible

            // Apply the animation
            var popupAnimation = (Storyboard)Resources["PopupAnimation"];
            popupAnimation.Begin(this);
            this.ShowDialog();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: () => jsonAdhkar.GetZekrByIdAsync(zekr_id),
                         onSuccess: result =>
                         {
                             _zekr = result;
                             this.zekrTitle.Text = result.category;
                             this.zekrDescription.Text = result.zekrContent[0].text;
                             this.zekrCounter.Value = result.zekrContent[0].count;
                         },
                         retryNumber: 2,
                         () => // handle an error
                         {

                         });
        }

        private void nextZekr_Click(object sender, RoutedEventArgs e)
        {
            NextZekr();
        }

        private void perviousZekr_Click(object sender, RoutedEventArgs e)
        {
            PrevZekr(); 
        }
    }
}

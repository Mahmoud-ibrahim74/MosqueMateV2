using HandyControl.Tools.Extension;
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
        private int ZekrIndex = 0;
        private int progressIncrement = 0;
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
            ZekrIndex++;
            if (ZekrIndex > _zekr.zekrContent.Count-1)
            {
                ZekrIndex = 0;
            }
            if (ZekrIndex < _zekr.zekrContent.Count)
            {
                zekrDescription.Text = _zekr.zekrContent[ZekrIndex].text;
                zekrCounterTxt.Text = _zekr.zekrContent[ZekrIndex].count.ToString();
                ResetCounter(); 
            }
        }
        private void PrevZekr()
        {

            if (ZekrIndex == 0)
                ZekrIndex = _zekr.zekrContent.Count - 1;
            ZekrIndex--;
            zekrDescription.Text = _zekr.zekrContent[ZekrIndex].text;
            zekrCounterTxt.Text = _zekr.zekrContent[ZekrIndex].count.ToString();
            ResetCounter();

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
                             this.zekrDescription.Text = result.zekrContent[ZekrIndex].text;
                             this.zekrCounterTxt.Text = _zekr.zekrContent[ZekrIndex].count.ToString();
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

        private void zekrDescription_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var currentZekr = _zekr.zekrContent[ZekrIndex];
            progressIncrement = 100 / currentZekr.count;
            zekrCounter.Value += progressIncrement;
        }
        private void ResetCounter()
        {
            progressIncrement = 0;
            zekrCounter.Value = 0;
        }
    }
}

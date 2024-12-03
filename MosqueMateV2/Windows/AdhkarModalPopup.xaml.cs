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
        DTOAdhkar ZekrContent { get; set; }
        RxTaskManger rxTaskManger;
        IJsonAdhkarRepository jsonAdhkar;

        public AdhkarModalPopup(int zekId)
        {
            InitializeComponent();
            rxTaskManger = new();
            this.zekr_id = zekId;
            jsonAdhkar = new JsonAdhkarRepository();
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
                             ZekrContent = result;
                             this.zekrTitle.Text = result.category;
                             this.zekrDescription.Text = result.zekrContent[0].text;
                         },
                         retryNumber: 2,
                         () => // handle an error
                         {

                         });
        }
    }
}

using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using System.Windows;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class AdhkarModalPopup : Window
    {
        DTOAdhkar zekrData { get; set; }
        public AdhkarModalPopup(/*DTOAdhkar data*/)
        {
            InitializeComponent();
            //this.zekrData = data;
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

        }
    }
}

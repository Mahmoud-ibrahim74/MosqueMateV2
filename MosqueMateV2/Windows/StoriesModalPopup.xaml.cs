using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using MosqueMateV2.Service.IServices;
using MosqueMateV2.Service.Services;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using static System.Windows.Forms.LinkLabel;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class StoriesModalPopup : Window
    {
        RxTaskManger rxTaskManger { get; set; } 
        public StoriesModalPopup(int pageIndex)
        {
            InitializeComponent();
            rxTaskManger = new RxTaskManger();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        { 
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.loader.Visibility = Visibility.Visible;  
            rxTaskManger.RunBackgroundTaskOnUI(
                        backgroundTask: token => FileHelper.WritePdfToTempAsync(FileResources.Stories),
                                    onSuccess: result =>
                                    {
                                        if (result is not null)
                                        {
                                            WebView.Source = new Uri(result);
                                            this.loader.Visibility = Visibility.Collapsed;
                                        }
                                    },
                                    retryNumber: 2,
                                    () => 
                                    {

                                    });
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
    
        }
        public void ShowModal()
        {
            this.Visibility = Visibility.Visible;  // Ensure the modal is visible
            this.Opacity = 0;  // Start as invisible

            // Apply the animation
            var popupAnimation = (Storyboard)Resources["PopupAnimation"];
            popupAnimation.Begin(this);
            this.ShowDialog();

        }

    }
}

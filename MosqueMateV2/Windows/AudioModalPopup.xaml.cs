using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Service.IServices;
using MosqueMateV2.Service.Services;
using System.Windows;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class AudioModalPopup : Window
    {
        string url { get; set; }
        int suraIndex { get; set; }
        RxTaskManger rxTaskManger { get; set; }
        ISuraRepository sura;
        IYoutubeService youtubeService { get; set; }
        IFileServices fileServices { get; set; }   
        IVLCService vLCService { get; set; }    
        public AudioModalPopup(string url,int suraIndex)
        {
            InitializeComponent();
            this.url = url;
            this.suraIndex = suraIndex; 
            rxTaskManger = new();  
            sura = new SuraRepository();
            youtubeService = new YoutubeService();
            fileServices =new FileServices();

        }
        // Show the modal with popup animation
        public void ShowModal()
        {
            this.Visibility = Visibility.Visible; 
            this.Opacity = 0;

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
            this.suraName.Text = sura.GetSuraById(suraIndex).name;
            this.IsEnabled = false;
            var audioName = fileServices.CombinePathWithTemp("Audio.mp3");
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: () => youtubeService.DownloadYouTubeVideoAsync(url, audioName),
                         onSuccess: result =>
                         {
                             this.IsEnabled = true;
                             vLCService = new VLCService(audioName);
                             audioView.MediaPlayer = vLCService._mediaPlayer;
                             vLCService.PlayMedia();
                         },
                         retryNumber: 2,
                         () => // handle an error
                         {

                         });
        }

        private void nextZekr_Click(object sender, RoutedEventArgs e)
        {
        }

        private void perviousZekr_Click(object sender, RoutedEventArgs e)
        {
        }

        private void zekrDescription_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void playAudio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void pauseAudio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stopAudio_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

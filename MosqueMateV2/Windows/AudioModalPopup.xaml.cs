using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using MosqueMateV2.Service.IServices;
using MosqueMateV2.Service.Services;
using System.IO;
using System.Windows;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class AudioModalPopup : Window
    {
        int suraIndex { get; set; }
        RxTaskManger rxTaskManger { get; set; }
        ISuraRepository sura;
        ILinkRepository linkRepository { get; set; }
        IYoutubeService youtubeService { get; set; }
        IFileServices fileServices { get; set; }
        INAudioService audioService { get; set; }
        
        public AudioModalPopup(int suraIndex)
        {
            InitializeComponent();
            this.suraIndex = suraIndex;
            rxTaskManger = new();
            sura = new SuraRepository();
            youtubeService = new YoutubeService();
            fileServices = new FileServices();
            linkRepository = new LinkRepository();
        }

        // Show the modal with popup animation
        public void ShowModal()
        {
            this.Visibility = Visibility.Visible;
            this.Opacity = 0;

            // Apply the animation
            var popupAnimation = (Storyboard)Resources["PopupAnimation"];
            popupAnimation.Begin(this);
            this.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            audioService?.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.suraName.Text = sura.GetSuraById(suraIndex).name ?? AppLocalization.DefaultSura;
            var link  = linkRepository.GetLinkByName(this.suraName.Text).url;
            var audioName = fileServices.CombinePathWithTemp(this.suraName.Text + ".mp3");
            this.Title = this.suraName.Text;
            if (File.Exists(audioName))
            {
                audioService = new NAudioService(audioName);
                audioService.PlayAudio();
            }
            else
            {
                DisableControls();
                rxTaskManger.RunBackgroundTaskOnUI(
                             backgroundTask: () => youtubeService.DownloadYouTubeAudioAsync(link, audioName),
                             onSuccess: result =>
                             {
                                 audioService = new NAudioService(audioName);
                                 audioService.PlayAudio();
                                 EnableControls();
                             },
                             retryNumber: 2,
                             () => // handle an error
                             {

                             });
            }
        }

        private void playAudio_Click(object sender, RoutedEventArgs e)
        {
            audioService?.PlayAudio();  
        }
        private void stopAudio_Click(object sender, RoutedEventArgs e)
        {
            audioService?.StopAudio();
        }

        private void EnableControls()
        {
            playAudio.IsEnabled = true;
            stopAudio.IsEnabled = true;
            loader.Visibility = Visibility.Collapsed;
            loadingTxt.Visibility = Visibility.Collapsed;   
        }
        private void DisableControls()
        {
            playAudio.IsEnabled = false;
            stopAudio.IsEnabled = false;
            loader.Visibility = Visibility.Visible;
            this.loadingTxt.Text = App.LocalizationService[AppLocalization.Connection];
            this.loadingTxt.Visibility = Visibility.Visible;
        }
    }
}

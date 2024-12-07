using LibVLCSharp.Shared;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class PlayListViewer : Window
    {
        string videoUrl { get; set; }
        RxTaskManger rxTaskManger { get; set; }
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        public PlayListViewer(string url)
        {
            InitializeComponent();
            videoUrl = url;
            rxTaskManger = new();
            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            this.vlcVideoView.MediaPlayer = _mediaPlayer;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.loader.Visibility = Visibility.Visible;
            this.gridContainer.IsEnabled = false;
            rxTaskManger.RunBackgroundTaskOnUI(
                         backgroundTask: () => YoutubeHelper.DownloadYouTubeVideoAsync(videoUrl),
                         onSuccess: result =>
                         {
                             this.loader.Visibility = Visibility.Hidden;
                             this.gridContainer.IsEnabled = true;
                             PlayVideo();
                         },
                         onError: () => // handle an error
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

        private void playVideo_Click(object sender, RoutedEventArgs e)
        {
            PlayVideo();
        }

        private void pauseVideo_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer?.Pause();
        }
        private void PlayVideo()
        {
            _mediaPlayer?.Play(new Media(_libVLC,
                AppLocalization.VideoDirectoryDownload,
                FromType.FromPath));

        }
        private void PauseVideo()
        {
            _mediaPlayer?.Pause();
        }
    }
}

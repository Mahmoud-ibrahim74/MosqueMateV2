using MosqueMateV2.Domain.Enums;
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
    public partial class QuranModalPopup : Window
    {
        int pageIndex { get; set; }
        string suraName { get; set; }   
        int index { get; set; }
        QuranResource quranRes;
        ISuraRepository _sura;
        RxTaskManger rxTaskManger { get; set; }
        ILinkRepository linkRepository { get; set; }
        IYoutubeService youtubeService { get; set; }
        IFileServices fileServices { get; set; }
        INAudioService audioService { get; set; }

        public QuranModalPopup(int pageIndex)
        {
            InitializeComponent();
            this.index = pageIndex;
            this.pageIndex = pageIndex;
            rxTaskManger = new RxTaskManger();
            quranRes = new QuranResource();
            _sura = new SuraRepository();
            youtubeService = new YoutubeService();
            fileServices = new FileServices();
            linkRepository = new LinkRepository();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppSettings.Default[nameof(AppSettings.Default.ContinueReading)] = index;
            AppSettings.Default.Save();


            if (rxTaskManger._cancellationTokenSource is not null && !rxTaskManger._cancellationTokenSource.IsCancellationRequested)
                rxTaskManger.StopBackgroundTask();

            if (audioService is not null)
                audioService?.Dispose();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.downloadAudio.Content = App.LocalizationService[AppLocalization.DownloadAudio];
            var resByte = quranRes.GetPageContent(index);
            imgViewer.ImageSource = ImageHelper.ConvertBytesToBitmapFrame(resByte);
            playAudio.ToolTip = App.LocalizationService[AppLocalization.Play];
            stopAudio.ToolTip = App.LocalizationService[AppLocalization.Stop];
            connectionTxt.Text = App.LocalizationService[AppLocalization.Connection];

            ChangeWindowTitle();
            LoadData();

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                e.Handled = true;
                PrevPage();
            }
            else if (e.Key == Key.Right)
            {
                e.Handled = true;
                NextPage();
            }
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        private void ChangeWindowTitle()
        {
            var suraName = _sura.GetSuraById(index);
            if (suraName is not null)
            {
                this.Title = suraName.name;
                this.suraName = suraName.name;
            }
        }
        private void NextPage()
        {
            if (imgViewer.ImageSource is not null)
            {
                if (index == 604)
                    index = 1;


                imgViewer.ImageSource = null;
                index++;
                var resByte = quranRes.GetPageContent(index);
                imgViewer.ImageSource = ImageHelper.ConvertBytesToBitmapFrame(resByte);
                ChangeWindowTitle();
            }
        }
        private void PrevPage()
        {
            if (imgViewer.ImageSource is not null)
            {
                if (index == 1)
                    index = 604;


                imgViewer.ImageSource = null;
                index--;
                var resByte = quranRes.GetPageContent(index);
                imgViewer.ImageSource = ImageHelper.ConvertBytesToBitmapFrame(resByte);
                ChangeWindowTitle();
            }

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

        private void downloadAudio_Click(object sender, RoutedEventArgs e)
        {
            DownloadAudio();
        }

        private void playAudio_Click(object sender, RoutedEventArgs e)
        {
            audioService?.PlayAudio();
        }
        private void stopAudio_Click(object sender, RoutedEventArgs e)
        {
            audioService?.StopAudio();
        }
        private void DownloadAudio()
        {
            this.loader.Visibility = Visibility.Visible;
            this.connectionTxt.Visibility = Visibility.Visible; 
            this.IsEnabled = false;
            this.imgViewer.Opacity = 0.2;


            #region ReceiterSetting
            var receiterSetting = EnumHelper<ReciterTypesEnum>.GetEnumValue(AppSettings.Default.Reciter);

            var link = receiterSetting switch
            {
                ReciterTypesEnum.Ofasy => linkRepository.GetLinkByName(this.suraName).url1,
                ReciterTypesEnum.Dosary => linkRepository.GetLinkByName(this.suraName).url2,
                _ => linkRepository.GetLinkByName(this.suraName).url1
            }; 
            #endregion



            var audioName = fileServices.CombinePathWithTemp(this.suraName + AppLocalization.Mp3_exe);
            rxTaskManger.RunBackgroundTaskOnUI(
                      backgroundTask: token => youtubeService.DownloadYouTubeAudioAsync(link, audioName),
                      onSuccess: result =>
                      {
                          if (File.Exists(audioName))
                          {
                              audioService?.Dispose();
                              audioService = new NAudioService(audioName);
                              downloadAudio.Visibility = Visibility.Collapsed;
                              playAudio.Visibility = Visibility.Visible;
                              stopAudio.Visibility = Visibility.Visible;
                              gridContainer.Children.Remove(loader);
                              gridContainer.Children.Remove(connectionTxt);    
                              this.IsEnabled = true;
                              this.imgViewer.Opacity = 1;

                          }
                      },
                      retryNumber: 2,
                      () => // handle an error
                      {

                      });
        }

        private void LoadData()
        {
            var suraName = _sura.GetSuraById(pageIndex).name ?? AppLocalization.DefaultSura;
            var link = linkRepository.GetLinkByName(suraName).url1;
            var audioName = fileServices.CombinePathWithTemp(suraName + AppLocalization.Mp3_exe);
            if (File.Exists(audioName))
            {
                audioService = new NAudioService(audioName);
                downloadAudio.Visibility = Visibility.Collapsed;
                playAudio.Visibility = Visibility.Visible;
                stopAudio.Visibility = Visibility.Visible;
            }
        }
    }
}

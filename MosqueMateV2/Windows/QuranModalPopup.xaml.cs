using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class QuranModalPopup : Window
    {
        int pageIndex { get; set; }
        int index { get; set; }
        QuranResource quranRes;
        ISuraRepository _sura;
        public QuranModalPopup(int pageIndex)
        {
            InitializeComponent();
            this.index = pageIndex;
            this.pageIndex = pageIndex; 
            quranRes = new QuranResource();
            _sura = new SuraRepository();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppSettings.Default[nameof(AppSettings.Default.ContinueReading)] = index;
            AppSettings.Default.Save();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.downloadAudio.Content = App.LocalizationService[AppLocalization.DownloadAudio];
            var resByte = quranRes.GetPageContent(index);
            imgViewer.ImageSource = ImageHelper.ConvertBytesToBitmapFrame(resByte);
            ChangeWindowTitle();
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
                this.Title = suraName.name;
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
            var modal = new AudioModalPopup(pageIndex);
            modal.ShowModal();
        }
    }
}

using MosqueMateV2.Helpers;
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
        QuranResource quranRes;
        RxTaskManger rxTaskManger;

        public QuranModalPopup(int pageIndex)
        {
            InitializeComponent();
            rxTaskManger = new();
            this.pageIndex = pageIndex;
            quranRes = new QuranResource();

        }
        private void NextPage()
        {
            if (imgViewer.ImageSource is not null)
            {
                if (pageIndex == 604)
                    pageIndex = 1;


                imgViewer.ImageSource = null;
                pageIndex++;
                var resByte = quranRes.GetPageContent(pageIndex);
                imgViewer.ImageSource = ImageHelper.ConvertBytesToBitmapFrame(resByte);
            }
        }
        private void PrevPage()
        {
            if (imgViewer.ImageSource is not null)
            {
                if (pageIndex == 1)
                    pageIndex = 604;


                imgViewer.ImageSource = null;
                pageIndex--;
                var resByte = quranRes.GetPageContent(pageIndex);
                imgViewer.ImageSource = ImageHelper.ConvertBytesToBitmapFrame(resByte);
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var resByte = quranRes.GetPageContent(pageIndex);
            imgViewer.ImageSource = ImageHelper.ConvertBytesToBitmapFrame(resByte);
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
        private void zekrDescription_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void ResetCounter()
        {

        }
    }
}

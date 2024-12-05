using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class QuranModalPopup : Window
    {
        int pageIndex { get; set; }
        RxTaskManger rxTaskManger;

        public QuranModalPopup(int pageIndex)
        {
            InitializeComponent();
            rxTaskManger = new();
            this.pageIndex = pageIndex;

        }
        private void NextZekr()
        {

        }
        private void PrevZekr()
        {


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
            var resByte = new QuranResource().GetPageContent(pageIndex);
            //zekrImage.Source = ImageHelper.ConvertBytesToImage(resByte);

            CoverFlowMain.AddRange(
            [
                ImageHelper.ConvertBytesToImage(resByte),
                new Uri(@"pack://application:,,,/Assets/pause.png"),
                new Uri(@"pack://application:,,,/Assets/pause.png"),
                new Uri(@"pack://application:,,,/Assets/pause.png"),
                new Uri(@"pack://application:,,,/Assets/pause.png"),
                new Uri(@"pack://application:,,,/Assets/pause.png"),
                new Uri(@"pack://application:,,,/Assets/pause.png"),
                new Uri(@"pack://application:,,,/Assets/pause.png"),
                new Uri(@"pack://application:,,,/Assets/pause.png") ,
                new Uri(@"pack://application:,,,/Assets/pause.png")
             ]);
            CoverFlowMain.Add(new Uri("pack://application:,,,/Assets/pause.png"));
            CoverFlowMain.Add(new Uri("pack://application:,,,/Assets/pause.png"));
            CoverFlowMain.Add(new Uri("pack://application:,,,/Assets/pause.png"));
            CoverFlowMain.Add(new Uri("pack://application:,,,/Assets/pause.png"));
            CoverFlowMain.Add(new Uri("pack://application:,,,/Assets/pause.png"));
            CoverFlowMain.Add(new Uri("pack://application:,,,/Assets/pause.png"));
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

        }
        private void ResetCounter()
        {

        }
    }
}

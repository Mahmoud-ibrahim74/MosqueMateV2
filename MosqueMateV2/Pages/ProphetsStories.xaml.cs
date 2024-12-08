using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProphetsStories : Page
    {

        RxTaskManger rxTaskManger;
        public IJsonAdhkarRepository jsonAdhkar;

        public ProphetsStories()
        {
            InitializeComponent();
            rxTaskManger = new();
            jsonAdhkar = new JsonAdhkarRepository();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // var main = Application.Current.MainWindow;
           // this.loader.Visibility = Visibility.Visible;
           //rxTaskManger.RunBackgroundTaskOnUI(
           //      backgroundTask: () => YoutubeHelper.GetPlayListAsync(AppLocalization.PlayListUrl),
           //      onSuccess: result =>
           //      {
           //          GridCardContainer.GenerateMaterialDesignCardsPlayList(result);
           //          this.loader.Visibility = Visibility.Hidden;
           //      },
           //      retryNumber: 2,
           //      () => // handle an error
           //      {
           //          this.loader.Visibility = Visibility.Hidden;
           //      });
        }
        private async void searchOnAdhan_Click(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(AdhkarSearchTxt.Text))
            //{
            //    var offest = GridCardContainer.GetOffsetChildOfElement(AdhkarSearchTxt.Text);
            //    if (offest.ControlPosition is not null)
            //    {
            //        adhkarScrollViewer.ScrollToVerticalOffset(offest.ControlPosition.Value.Y - 300);
            //        if (offest.SelectedElement is not null)
            //        {
            //            AnimationHelper animation = new(AppLocalization.AnimationDuration);
            //            animation.CreatePulseAnimation(offest.SelectedElement);
            //            animation.StartPulseAnimation();
            //        }
            //    }
            //}
        }
    }
}
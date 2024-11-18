using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Helpers;
using MosqueMateV2.Helpers.AppHelpers;
using Resources;
using System.Windows;
using XamlAnimatedGif;

namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string apiContent { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ApiClient.Configure("cairo", "egypt", 2);
            this.Title = App.LocalizationService[AppLocalization.MainMenu];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            this.IsEnabled = false;
            TaskHelper.RunBackgroundTaskOnUI(
                 backgroundTask: () => ApiClient.GetAsync(),
                 onSuccess: result =>
                 {
                     apiContent = result;
                     this.IsEnabled = true;
                     this.LoaderGif.Visibility = Visibility.Hidden;
                     LoadData();
                 });
        }
        private void LoadData()
        {
            this.mildaiDate.Content = DateTime.Now.ToLongDateString();
            this.hijiriDate.Content = DateTime.Now.ToLongDateString();
            this.title.Content = App.LocalizationService[AppLocalization.AppName];
        }

    }
}
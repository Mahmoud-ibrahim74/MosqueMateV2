using MosqueMateV2.Domain.APIService;
using MosqueMateV2.Helpers.AppHelpers;
using Resources;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Windows;

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
            this.IsEnabled = false;  
            TaskHelper.RunBackgroundTaskOnUI(
                 backgroundTask: () => ApiClient.GetAsync(),
                 onSuccess: result =>
                 {
                     apiContent = result;
                     Thread.Sleep(5000);
                     this.IsEnabled = true;
                 });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(apiContent);
        }
    }
}
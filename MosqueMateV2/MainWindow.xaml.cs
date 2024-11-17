using MosqueMateV2.Domain.APIService;
using Resources;
using System.Windows;

namespace MosqueMateV2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApiClient.Configure("cairo", "egypt", 2);
            this.Title = App.LocalizationService[AppLocalization.MainMenu];
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var res = await ApiClient.GetAsync();

        }
    }
}
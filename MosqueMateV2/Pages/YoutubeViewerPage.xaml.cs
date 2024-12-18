using MosqueMateV2.Helpers;
using System.Windows;
using System.Windows.Input;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class YoutubeViewerPage : Page
    {
        string VidoeUrl {  get; set; }  
        public YoutubeViewerPage(string videUrl)
        {
            InitializeComponent();
            this.VidoeUrl = videUrl;    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WebView.Source = new Uri(VidoeUrl);
            WebView.Focus();    
        }
  
        private void WebView_KeyDown(object sender, KeyEventArgs e)
        {
            KeyboardHelper.ActionPressCTRLKey(e,Key.B, () =>
            {
                AppHelper.GoBack(); 
            });
        }

    }
}
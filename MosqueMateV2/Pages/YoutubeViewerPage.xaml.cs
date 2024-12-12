using ModernWpf.Controls;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
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

        RxTaskManger rxTaskManger;
        ViewTypesEnum _Type;
        string VidoeUrl {  get; set; }  
        public YoutubeViewerPage(string videUrl)
        {
            InitializeComponent();
            rxTaskManger = new();
            this.VidoeUrl = videUrl;    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WebView.Source = new Uri(VidoeUrl);
        }
  
        private void WebView_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.B && Keyboard.IsKeyDown(Key.LeftCtrl)) || 
                (e.Key == Key.B && Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                AppHelper.BackToHome();
            }
        }

    }
}
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
    public partial class PdfViewerPage : Page
    {

        RxTaskManger rxTaskManger;
        ViewTypesEnum _Type;
        public PdfViewerPage(ViewTypesEnum types)
        {
            InitializeComponent();
            rxTaskManger = new();
            this._Type = types;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.loader.Visibility = Visibility.Visible;
            var fileByte = this._Type switch
            {
                ViewTypesEnum.Stories => FileResources.Stories,
                ViewTypesEnum.Rokya => FileResources.rokya,
                ViewTypesEnum.FortyNawawi => FileResources.nawawi40,
                ViewTypesEnum.Kodosy =>FileResources.Qdssiah,
                _ => FileResources.Stories
            };

            rxTaskManger.RunBackgroundTaskOnUI(
          backgroundTask: token => FileHelper.WritePdfToTempAsync(fileByte),
                        onSuccess: result =>
                        {
                            if (result is not null)
                            {
                                WebView.Source = new Uri(result);
                                this.loader.Visibility = Visibility.Collapsed;
                                this.WebView.Focus();   
                            }
                        },
                        retryNumber: 2,
                        () => // handle an error
                        {
                            this.loader.Visibility = Visibility.Collapsed;

                        });
        }
  
        private void WebView_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.B && Keyboard.IsKeyDown(Key.LeftCtrl)) || (e.Key == Key.B && Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                BackToHome();
            }
        }
        private void BackToHome()
        {
            var window = App.Current.MainWindow;
            if (window is not null)
            {
                var frame = window.FindName("MainFrame") as Frame;
                frame?.Navigate(new Home());
                var nav = window.FindName("NavView") as NavigationView;
                var homeItem = window.FindName("homeItem") as NavigationViewItem;
                if (nav is not null && homeItem is not null)
                    nav.SelectedItem = homeItem;
            }
        }
    }
}
using ModernWpf.Controls;
using MosqueMateV2.Domain.Enums;
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
            ToastNotificationsHelper.
            SendNotification(
                        title: "Info",
                        message: "Press Alt + ← to Back",
                        duration: new TimeSpan(0, 0, SD.Localization.NotificatonDuration),
                        type: Notification.Wpf.NotificationType.Information);
            this.loader.Visibility = Visibility.Visible;
            var fileByte = this._Type switch
            {
                ViewTypesEnum.Stories => FileResources.Stories,
                ViewTypesEnum.Rokya => FileResources.rokya,
                ViewTypesEnum.FortyNawawi => FileResources.nawawi40,
                ViewTypesEnum.Kodosy =>FileResources.Qdssiah,
                ViewTypesEnum.Wasaya =>FileResources.Wasaya,
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
            KeyboardHelper.ActionPressCTRLKey(e, Key.B, () =>
            {
                AppHelper.GoHome();
            });
        }
    }
}
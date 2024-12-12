using Microsoft.Web.WebView2.Core;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using MosqueMateV2.Service.Services;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using Window = System.Windows.Window;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        int progress = 0;

        RxTaskManger rxTaskManger { get; set; }
        public TestWindow()
        {
            InitializeComponent();
            rxTaskManger = new();
        }

        private void ConfirmationDialog_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rxTaskManger.RunBackgroundTaskOnUI(
          backgroundTask: token => FileHelper.WritePdfToTempAsync(FileResources.Stories),
                        onSuccess: result =>
                        {
                            if(result is not null)
                            {
                                WebView.Source = new Uri(result);

                            }
                        },
                        retryNumber: 2,
                        () => // handle an error
                        {

                        });

        }
    }
}

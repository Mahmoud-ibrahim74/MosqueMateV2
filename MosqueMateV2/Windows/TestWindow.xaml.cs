using System.Windows;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            InitializeComponent();
        }

        private void ConfirmationDialog_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void PowerButton_Click(object sender, RoutedEventArgs e)
        {
            var message =new HandyControl.Data.MessageBoxInfo();
            message.Message = "hello";
            message.Button = MessageBoxButton.YesNo;
            message.Caption = "No ...";

            //HandyControl.Controls.MessageBox.Show(message);
            //dailog.Show();
            AdhkarModalPopup modalPopup = new(1);
            modalPopup.ShowModal();
        }
    }
}

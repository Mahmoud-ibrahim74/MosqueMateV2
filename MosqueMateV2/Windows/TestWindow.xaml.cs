using MaterialDesignThemes.Wpf;
using Notifications.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            AdhkarModalPopup modalPopup = new();
            modalPopup.ShowModal();
        }
    }
}

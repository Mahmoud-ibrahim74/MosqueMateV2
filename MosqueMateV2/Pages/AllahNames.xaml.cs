using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
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
    public partial class AllahNames : Page
    {

        RxTaskManger rxTaskManger;
        public IJsonAllahNamesRepository _names;

        public AllahNames()
        {
            InitializeComponent();
            rxTaskManger = new();
            _names = new JsonAllahNamesRepository();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var main = Application.Current.MainWindow;

            rxTaskManger.RunBackgroundTaskOnUI(
                 backgroundTask: token => _names.GetAllNames(),
                 onSuccess: result =>
                 {
                     GridCardContainer.GenerateCardsForAllahNames(
                                    data: result
                                );
                     GridCardContainer.Focus();
                 },
                 retryNumber: 2,
                 () => // handle an error
                 {

                 });
        }

        private void GridCardContainer_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyboardHelper.ActionPressCTRLKey(e, Key.B, () =>
            {
                AppHelper.GoBack();
            });
        }
    }
}
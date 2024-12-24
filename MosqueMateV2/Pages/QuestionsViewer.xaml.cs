using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class QuestionsViewer : Page
    {

        RxTaskManger rxTaskManger;
        HistoricTypesEnum _historicTypes;
        IResourceManagerRepository resourceManager {  get; set; }   
        public QuestionsViewer(HistoricTypesEnum type)
        {
            InitializeComponent();
            rxTaskManger = new();
            this._historicTypes = type;
            resourceManager = new ResourceManagerRepository(ResourceTypeEnum.FileResources);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
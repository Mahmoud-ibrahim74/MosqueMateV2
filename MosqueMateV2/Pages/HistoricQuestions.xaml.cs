using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using System.Windows;
using Page = ModernWpf.Controls.Page;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HistoricQuestions : Page
    {

        RxTaskManger rxTaskManger;
        Dictionary<HistoricTypesEnum, string> historicDic { get; set; } = [];

        public HistoricQuestions()
        {
            InitializeComponent();
            rxTaskManger = new();
            historicDic.Add(HistoricTypesEnum.abasi, "العصر العباسي");
            historicDic.Add(HistoricTypesEnum.amwi, "العصر الأموى");
            historicDic.Add(HistoricTypesEnum.kholfa, "عصر الخلفاء");
            historicDic.Add(HistoricTypesEnum.mamalik, "العصر المماليك");
            historicDic.Add(HistoricTypesEnum.moasir, "العصر المعاصر");
            historicDic.Add(HistoricTypesEnum.osmany, "العصر العثماني");
            historicDic.Add(HistoricTypesEnum.sirah, "السيرة النبوية");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridCardContainer.GenerateCards(
                          data: historicDic,
                          PaddingTopTxt: 30,
                          CardWidth: 380,
                          CardHeight: 150,
                          FontSize: 10,
                          PaddingRightTxt: 15,
                          PaddingLeftTxt: 15,
                          textWrapping: TextWrapping.Wrap,
                          serviceType: PagesTypesEnum.HistoricQuestion
                      );
        }
    }
}
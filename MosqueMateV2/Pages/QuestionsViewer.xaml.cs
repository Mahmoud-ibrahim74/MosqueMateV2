using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Helpers;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Controls;

namespace MosqueMateV2.Pages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class QuestionsViewer : Page
    {

        RxTaskManger rxTaskManger;
        HistoricTypesEnum _historicTypes;
        IJsonQuestionRepository _jsonQuestion;
        int _level;
        int questionIndex = 0;
        List<DTOQuestions> questionList = [];
        public QuestionsViewer()
        {
            _level = new Random().Next(1,3);
            InitializeComponent();
            rxTaskManger = new();
            _jsonQuestion = new JsonQuestionRepository(EnumHelper<HistoricTypesEnum>.GetRandomValue(), _level);
            questionList = _jsonQuestion.GetAllQuestions();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var question = questionList.FirstOrDefault();
            FillQuestion(question);

        }
        private void FillQuestion(DTOQuestions question)
        {
            if (question is not null)
            {
                questionCountTxt.Text = $"Qo . {questionIndex + 1} / {questionList.Count}";
                questionTxt.Text = question.q;
                ans1.Content = question.answers[0].answer;
                ans2.Content = question.answers[1].answer;
                ans3.Content = question.answers[2].answer;
            }
        }
    }
}
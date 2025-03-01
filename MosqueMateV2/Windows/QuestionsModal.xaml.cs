using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Domain.Repositories;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using MosqueMateV2.Properties;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Media.Animation;

namespace MosqueMateV2.Windows
{
    /// <summary>
    /// Interaction logic for ModalPopup.xaml
    /// </summary>
    public partial class QuestionsModal : Window
    {
        DTOQuestions currentQuestion {  get; set; } 
        public QuestionsModal()
        {
            #region SetTheme
            this.ThemeMode = OSHelper.GetWindowsTheme();
            #endregion
            InitializeComponent();

        }
        public void ShowModal()
        {

            this.Visibility = Visibility.Visible;  // Ensure the modal is visible
            this.Opacity = 0;  // Start as invisible

            // Apply the animation
            var popupAnimation = (Storyboard)Resources["PopupAnimation"];
            popupAnimation.Begin(this);
            this.ShowDialog();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = App.LocalizationService[SD.Localization.TestYourSelf];
            var level = new Random().Next(1,3);
            var type = EnumHelper<HistoricTypesEnum>.GetRandomValue();
            using IJsonQuestionRepository jsonQuestion = new JsonQuestionRepository(type, level);
            var res = jsonQuestion.GetRandomQuestion();
            currentQuestion = res;
            questionTitle.Text = App.LocalizationService[SD.Localization.TestYourSelf];
            questionTxt.Text = res.q;
            ans1.Content = res.answers[0].answer;
            ans2.Content = res.answers[1].answer;
            ans3.Content = res.answers[2].answer;
        }

        private void checkAnswer_Click(object sender, RoutedEventArgs e)
        {
            var correctAnswer = currentQuestion.answers.FirstOrDefault(x => x.t == 1);
            if (correctAnswer != null)
            {
                var index = currentQuestion.answers.IndexOf(correctAnswer);
                switch (index)
                {
                    case 0:
                        AnsSolutionIcon.Margin = new Thickness(500, 300, 31, 254);
                        ans1.IsChecked = true;
                        ans2.IsEnabled = false;
                        ans3.IsEnabled = false;
                        break;           
                    case 1:
                        AnsSolutionIcon.Margin = new Thickness(500, 380,52,100);
                        ans2.IsChecked = true;
                        ans1.IsEnabled = false;
                        ans3.IsEnabled = false;
                        break;         
                    case 2:
                        AnsSolutionIcon.Margin = new Thickness(500, 455, 49, 110);
                        ans3.IsChecked = true;
                        ans1.IsEnabled = false;
                        ans2.IsEnabled = false;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}

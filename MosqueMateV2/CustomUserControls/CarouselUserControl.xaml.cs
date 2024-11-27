using MosqueMateV2.DataAccess.Models;
using MosqueMateV2.Extensions;
using MosqueMateV2.Helpers;
using Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MosqueMateV2.CustomUserControls
{
    /// <summary>
    /// Interaction logic for CarouselUserControl.xaml
    /// </summary>
    public partial class CarouselUserControl : UserControl
    {
        private readonly RxTaskManger _taskManger;
        private bool IsNighSet { get; set; }
        public CarouselUserControl()
        {
            InitializeComponent();
            _taskManger = new();
        }
        #region Dependency Property
        // Dependency Property for PrayerSlides
        public static readonly DependencyProperty PrayerSlidesProperty =
            DependencyProperty.Register(nameof(PrayerSlides), typeof(List<PrayerSlide>), typeof(CarouselUserControl),
                new PropertyMetadata(new List<PrayerSlide>()));

        /// <summary>
        /// The collection of slides to display in the carousel.
        /// </summary>
        public List<PrayerSlide> PrayerSlides
        {
            get => (List<PrayerSlide>)GetValue(PrayerSlidesProperty);
            set => SetValue(PrayerSlidesProperty, value);
        }
        #endregion
        private int _currentIndex = 0;

        /// <summary>
        /// Handles the "Previous" button click to move to the previous slide.
        /// </summary>
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (PrayerSlides.Any())
            {
                // Use LINQ to calculate the previous slide index
                _currentIndex = (_currentIndex - 1 + PrayerSlides.Count) % PrayerSlides.Count;
                UpdateCarousel();
            }
        }

        /// <summary>
        /// Handles the "Next" button click to move to the next slide.
        /// </summary>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (PrayerSlides.Any())
            {
                // Use LINQ to calculate the next slide index
                _currentIndex = (_currentIndex + 1) % PrayerSlides.Count;
                UpdateCarousel();
            }
        }

        /// <summary>
        /// Updates the carousel to display the slide at the current index.
        /// </summary>
        private void UpdateCarousel()
        {
            // Rearrange the slides to start from _currentIndex
            var reorderedSlides = PrayerSlides.Skip(_currentIndex)
                                              .Concat(PrayerSlides.Take(_currentIndex))
                                              .ToArray();
            ItemsControlSlides.ItemsSource = reorderedSlides;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _taskManger.StartUITaskScheduler(
                async()=> await Task.CompletedTask,
                TimeSpan.FromSeconds(1),
                CheckAdhanNow
                );
            _taskManger.StartUITaskScheduler(
                        async () => await Task.CompletedTask,
                        TimeSpan.FromSeconds(1),
                        NightMode
                        );
        }
        private void CheckAdhanNow()
        {
            if (AdhanHelper.IsAdhanNow)
            {
                toggleAdhan.Visibility = Visibility.Visible;
                var adhan = MediaResources.AbdelBastAbdelSamd;
                App.mP3Player.Play(adhan);
            }
        }
        private void NightMode()
        {
            if (App.Api_Response is not null)
            {
                if (!IsNighSet)
                {
                    var img = new BitmapImage(new Uri(DateTimeHelper.GetNightOfDay(App.Api_Response.Data.Timings.Maghrib)));
                    this.timeNow.Source = img;
                    IsNighSet = true;
                }
            }
        }
        private void toggleAdhan_Click(object sender, RoutedEventArgs e)
        {

            if (toggleAdhan.IsChecked == true)
            {
                toggleAdhan.AddImageToButton("pack://application:,,,/Assets/pause.png");
                App.mP3Player.Pause();
                toggleAdhan.ToolTip = App.LocalizationService[AppLocalization.Pause];
            }
            else if (toggleAdhan.IsChecked == false)
            {
                toggleAdhan.AddImageToButton("pack://application:,,,/Assets/play.png");
                App.mP3Player.Play();
                toggleAdhan.ToolTip = App.LocalizationService[AppLocalization.Play];

            }
        }
    }
}

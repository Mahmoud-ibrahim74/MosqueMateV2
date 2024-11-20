using MosqueMateV2.DataAccess.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MosqueMateV2.CustomUserControls
{
    /// <summary>
    /// Interaction logic for CarouselUserControl.xaml
    /// </summary>
    public partial class CarouselUserControl : UserControl
    {
        public CarouselUserControl()
        {
            InitializeComponent();
        }
        #region Dependency Property
        // Dependency Property for PrayerSlides
        public static readonly DependencyProperty PrayerSlidesProperty =
            DependencyProperty.Register(nameof(PrayerSlides), typeof(ObservableCollection<PrayerSlide>), typeof(CarouselUserControl),
                new PropertyMetadata(new ObservableCollection<PrayerSlide>()));

        /// <summary>
        /// The collection of slides to display in the carousel.
        /// </summary>
        public ObservableCollection<PrayerSlide> PrayerSlides
        {
            get => (ObservableCollection<PrayerSlide>)GetValue(PrayerSlidesProperty);
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
    }
}

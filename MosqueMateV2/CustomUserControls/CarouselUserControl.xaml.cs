using System.Collections;
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
        // Dependency Property for Slides
        public static readonly DependencyProperty SlidesProperty =
            DependencyProperty.Register(
                "Slides",
                typeof(ObservableCollection<Slide>),
                typeof(CarouselUserControl),
                new PropertyMetadata(null));

        /// <summary>
        /// The collection of slides to display in the carousel.
        /// </summary>
        public ObservableCollection<Slide> Slides
        {
            get { return (ObservableCollection<Slide>)GetValue(SlidesProperty); }
            set { SetValue(SlidesProperty, value); }
        }
        // Current index to keep track of the visible slide
        private int _currentIndex = 0;

        /// <summary>
        /// Handles the "Previous" button click to move to the previous slide.
        /// </summary>
        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (Slides is IList slidesList && slidesList.Count > 0)
            {
                _currentIndex = (_currentIndex - 1 + slidesList.Count) % slidesList.Count;
                UpdateCarousel(slidesList);
            }
        }

        /// <summary>
        /// Handles the "Next" button click to move to the next slide.
        /// </summary>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (Slides is IList slidesList && slidesList.Count > 0)
            {
                _currentIndex = (_currentIndex + 1) % slidesList.Count;
                UpdateCarousel(slidesList);
            }
        }

        /// <summary>
        /// Updates the carousel to display the slide at the current index.
        /// </summary>
        private void UpdateCarousel(IList slidesList)
        {
            if (slidesList.Count > 0)
            {
                var currentSlide = new[] { slidesList[_currentIndex] };
                SlideContainer.DataContext = currentSlide;
            }
        }
    }

    public class Slide
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}

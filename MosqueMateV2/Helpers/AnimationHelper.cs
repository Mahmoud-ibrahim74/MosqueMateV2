using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace MosqueMateV2.Helpers
{
    public class AnimationHelper 
    {
        private Storyboard _pulseStoryboard;
        private DispatcherTimer _timer;
        private bool IsPlaying { get; set; }
        public AnimationHelper(int durationInSeconds)
        {
            _pulseStoryboard = new();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(durationInSeconds)
            };
            _timer.Tick += OnTimerTick;
        }
        public void CreatePulseAnimation(UIElement element)
        {
            // Create a ScaleTransform and assign it to the element
            var scaleTransform = new ScaleTransform(1, 1);
            element.RenderTransform = scaleTransform;
            element.RenderTransformOrigin = new Point(0.5, 0.5);

            // Create ScaleX animation
            var scaleXAnimation = new DoubleAnimation
            {
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            // Create ScaleY animation
            var scaleYAnimation = new DoubleAnimation
            {
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.5),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            // Create Storyboard
            _pulseStoryboard.Children.Add(scaleXAnimation);
            _pulseStoryboard.Children.Add(scaleYAnimation);

            // Set target properties
            Storyboard.SetTarget(scaleXAnimation, element);
            Storyboard.SetTarget(scaleYAnimation, element);
            Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));
            IsPlaying = true;
        }

        public void StartPulseAnimation()
        {
            if (!IsPlaying)
                return;

            _pulseStoryboard?.Begin(); // Start the animation
            _timer.Start();       // Start the timer
        }
        private void OnTimerTick(object sender, EventArgs e)
        {
            _timer?.Stop();         // Stop the timer
            _pulseStoryboard?.Stop();    // Stop the animation
        }
    }
}

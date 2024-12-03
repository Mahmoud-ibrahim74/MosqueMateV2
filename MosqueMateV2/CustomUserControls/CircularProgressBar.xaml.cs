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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MosqueMateV2.CustomUserControls
{
    /// <summary>
    /// Interaction logic for CircularProgressBar.xaml
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        public CircularProgressBar()
        {
            InitializeComponent();
            DataContext = this;
        }

        // Dependency Property for Progress
        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register(
                "Progress",
                typeof(double),
                typeof(CircularProgressBar),
                new PropertyMetadata(0.0, OnProgressChanged));

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public Point StartPoint => new(Width / 2, StrokeThickness / 2);
        public Size ArcSize => new((Width - StrokeThickness) / 2, (Height - StrokeThickness) / 2);
        public bool IsLargeArc => Progress > 50;

        public Point ArcPoint
        {
            get
            {
                double angle = Progress / 100 * 360;
                double radians = Math.PI * angle / 180.0;

                double x = Width / 2 + ArcSize.Width * Math.Sin(radians);
                double y = Height / 2 - ArcSize.Height * Math.Cos(radians);

                return new Point(x, y);
            }
        }

        public double StrokeThickness => 10; // Thickness for the circular arcs

        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CircularProgressBar control)
            {
                control.ProgressLabel.Content = control.Progress.ToString();
                control.InvalidateVisual(); // Redraw the control
            }
        }
    }
}

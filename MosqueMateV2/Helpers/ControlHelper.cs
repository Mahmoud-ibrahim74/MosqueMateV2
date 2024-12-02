using MaterialDesignThemes.Wpf;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace MosqueMateV2.Helpers
{
    public static class ControlHelper
    {
        public static void GenerateMaterialDesignCards(this Grid grid, List<DTOAdhkar> data)
        {
            WrapPanel cardPanel = new()
            {
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            // Generate 20 Material Design cards
            foreach (var item in data)
            {
                Card card = new()
                {
                    Width = 200,
                    Height = 150,
                    Margin = new Thickness(10),
                    Background = CreateGradientBackground(),
                    Padding = new Thickness(16),
                    Foreground = new SolidColorBrush(Colors.White),
                    Cursor = Cursors.Hand
                };
                //card.Effect = new DropShadowEffect
                //{
                //    Color = Colors.White, // rgba(0, 0, 0, 0.12) in WPF format
                //    Opacity = 0.8,
                //    BlurRadius = 25,
                //    ShadowDepth = 4,
                //    Direction = 270 
                //};


                // Create card content
                StackPanel contentPanel = new();
                TextBlock title = new()
                {
                    Text = item.category,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 8),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    FlowDirection = FlowDirection.RightToLeft,
                    TextAlignment = TextAlignment.Center,
                    Padding = new Thickness(0, 20, 0, 0),
                    LineHeight = 30
                };
                contentPanel.Children.Add(title);
                card.Content = contentPanel;
                cardPanel.Children.Add(card);
            }
            grid.Children.Add(cardPanel);
        }

        private static LinearGradientBrush CreateGradientBackground()
        {
            return new LinearGradientBrush
            {
                GradientStops =
                        [
                            new GradientStop(Color.FromRgb(26,132,184), 0.0), // Orange
                            new GradientStop(Color.FromRgb(53,76,124), 1.0)  // Yellow
                        ],
                StartPoint = new Point(0, 0),  // Top-left corner
                EndPoint = new Point(1, 1)    // Bottom-right corner
            };
        }

    }
}

using MaterialDesignThemes.Wpf;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MosqueMateV2.Helpers
{
    public static class ControlHelper
    {
        /// <summary>
        /// Generates Material Design cards for the given data and adds them to the specified Grid.
        /// This method is designed to be generic and works for different types of data by utilizing
        /// lambda functions to extract required properties and a service type to determine the card's behavior.
        /// </summary>
        /// <typeparam name="T">The type of the data items used to generate the cards.</typeparam>
        /// <param name="grid">The Grid control where the cards will be added.</param>
        /// <param name="data">The list of data items used to create the cards.</param>
        /// <param name="getName">A function to extract the display name or title for each card from a data item.</param>
        /// <param name="getId">A function to extract the unique identifier for each card from a data item.</param>
        /// <param name="serviceType">An enum value indicating the type of service (e.g., Adhkar, Quran) for the cards.</param>
        /// <param name="CardWidth">Determine width of card</param>
        /// <param name="CardHeight">Determine height of card</param>
        public static void GenerateMaterialDesignCards<T>(
        this Grid grid,
        List<T> data,
        Func<T, string> getName,
        Func<T, int> getId,
        ServiceTypeEnum serviceType,
        int CardWidth  = 250,
        int CardHeight = 150
            )
        {
            WrapPanel cardPanel = new()
            {
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            foreach (var item in data)
            {
                Card card = new()
                {
                    Name = $"card_{getId(item)}",
                    Width = CardWidth,
                    Height = CardHeight,
                    Margin = new Thickness(10),
                    Background = CreateGradientBackground(),
                    Padding = new Thickness(16),
                    Foreground = new SolidColorBrush(Colors.White),
                    Cursor = Cursors.Hand,
                    FlowDirection = FlowDirection.RightToLeft
                };
                card.MouseLeftButtonDown += (sender, e) => Card_MouseLeftButtonDown(sender, e, serviceType);

                // Create card content
                StackPanel contentPanel = new();
                TextBlock title = new()
                {
                    Text = getName(item),
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 0, 0, 8),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    FlowDirection = FlowDirection.RightToLeft,
                    TextAlignment = TextAlignment.Center,
                    Padding = new Thickness(0, 40, 0, 0),
                    LineHeight = 30
                };
                contentPanel.Children.Add(title);
                card.Content = contentPanel;
                cardPanel.Children.Add(card);
            }

            grid.Children.Add(cardPanel);
        }

        private static void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, ServiceTypeEnum service)
        {
            switch (service)
            {
                case ServiceTypeEnum.Quran:
                    OpenQuranModal(sender as Card);
                    break;
                case ServiceTypeEnum.Adhkar:
                    OpenHadithModal(sender as Card);
                    break;
                default:
                    break;
            }

        }

        public static ControlBinding<Card> GetOffsetChildOfElement(this Grid grid, string childName)
        {
            if (grid == null || string.IsNullOrWhiteSpace(childName))
                return new()
                {
                    ControlPosition = new Point(10, 10),
                };

            var wrapPanel = grid.Children.OfType<WrapPanel>().FirstOrDefault();

            if (wrapPanel == null)
                return new()
                {
                    ControlPosition = new Point(10, 10),
                };
            foreach (var card in wrapPanel.Children)
            {
                if (card is Card cardPanel)
                {
                    // Look for the StackPanel inside the card
                    var stackPanel = cardPanel.Content as StackPanel;
                    if (stackPanel is not null)
                    {
                        // Look for the TextBlock inside the StackPanel
                        var textBlock = stackPanel.Children.OfType<TextBlock>()
                            .FirstOrDefault(tb => tb.Text.Contains(childName));
                        if (textBlock is not null)
                        {

                            return new()
                            {
                                ControlPosition = textBlock.TranslatePoint(new Point(0, 0), grid),
                                SelectedElement = cardPanel
                            };
                        }
                    }
                }
            }
            return new()
            {
                ControlPosition = new Point(10, 10),
            };
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

        private static void OpenHadithModal(Card selectedCard)
        {
            if (selectedCard is not null)
            {
                var id = NumberHelper.IsTextHasDigit(selectedCard.Name) ?
                    NumberHelper.GetTextFromDigit(selectedCard.Name) : 1;
                new AdhkarModalPopup(id).ShowModal();
            }
        }
        private static void OpenQuranModal(Card selectedCard)
        {
            if (selectedCard is not null)
            {
                var index = NumberHelper.IsTextHasDigit(selectedCard.Name) ?
                    NumberHelper.GetTextFromDigit(selectedCard.Name) : 1;
                var modal = new QuranModalPopup(index)
                {

                };
                modal.ShowModal();
            }
        }
    }


    public class ControlBinding<T>
    {
        public Point? ControlPosition { get; set; }
        public T? SelectedElement { get; set; }
    }
}






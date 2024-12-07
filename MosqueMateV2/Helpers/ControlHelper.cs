using MaterialDesignThemes.Wpf;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Extensions;
using MosqueMateV2.Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using YoutubeExplode.Playlists;

namespace MosqueMateV2.Helpers
{
    public static class ControlHelper
    {
        public static void GenerateMaterialDesignCardsForAdhkar(this Grid grid, List<DTOAdhkar> data)
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
                    Name = $"card_{item.id}",
                    Width = 250,
                    Height = 150,
                    Margin = new Thickness(10),
                    Background = CreateGradientBackground(),
                    Padding = new Thickness(16),
                    Foreground = new SolidColorBrush(Colors.White),
                    Cursor = Cursors.Hand
                };
                card.MouseLeftButtonDown += (sender, e) => Card_MouseLeftButtonDown(sender, e, ServiceTypeEnum.Adhkar);

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
                    Padding = new Thickness(0, 40, 0, 0),
                    LineHeight = 30
                };
                contentPanel.Children.Add(title);
                card.Content = contentPanel;
                cardPanel.Children.Add(card);
            }
            grid.Children.Add(cardPanel);
        }
        public static void GenerateMaterialDesignCardsForQuran(this Grid grid, List<DTOSuraNames> data)
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
                    Name = $"card_{item.pageIndex}",
                    Width = 250,
                    Height = 150,
                    Margin = new Thickness(10),
                    Background = CreateGradientBackground(),
                    Padding = new Thickness(16),
                    Foreground = new SolidColorBrush(Colors.White),
                    Cursor = Cursors.Hand,
                    FlowDirection = FlowDirection.RightToLeft
                };
                card.MouseLeftButtonDown += (sender, e) => Card_MouseLeftButtonDown(sender, e, ServiceTypeEnum.Quran);

                // Create card content
                StackPanel contentPanel = new();
                TextBlock title = new()
                {
                    Text = item.name,
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
        public static void GenerateMaterialDesignCardsPlayList(this Grid grid, IReadOnlyList<PlaylistVideo> playlists)
        {
            WrapPanel cardPanel = new()
            {
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            foreach (var item in playlists)
            {
                Card card = new()
                {
                    Uid = item.Url,
                    Width = 600,
                    Height = 150,
                    Margin = new Thickness(10),
                    Background = CreateGradientBackground(),
                    Padding = new Thickness(16),
                    Foreground = new SolidColorBrush(Colors.White),
                    Cursor = Cursors.Hand,
                    FlowDirection = FlowDirection.RightToLeft
                };
                card.MouseLeftButtonDown += (sender, e) => Card_MouseLeftButtonDown(sender, e, ServiceTypeEnum.ProphertStories);

                // Create card content
                StackPanel contentPanel = new();
                TextBlock title = new()
                {
                    Text = item.Title,
                    FontSize = 18,
                    FontWeight = FontWeights.UltraBold,
                    Margin = new Thickness(0, 0, 0, 8),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    FlowDirection = FlowDirection.RightToLeft,
                    TextAlignment = TextAlignment.Center,
                    Padding = new Thickness(0, 10, 0, 0),
                    LineHeight = 30,
                };
                TextBlock duration = new()
                {
                    Text = "".AppendDuration(item.Duration ?? TimeSpan.MinValue),
                    FontSize = 18,
                    FontWeight = FontWeights.UltraBold,
                    Margin = new Thickness(0, 10, 0, 8),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    FlowDirection = FlowDirection.RightToLeft,
                    TextAlignment = TextAlignment.Center,
                    LineHeight = 30,
                };
                contentPanel.Children.Add(title);
                contentPanel.Children.Add(duration);
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
                case ServiceTypeEnum.ProphertStories:
                    OpenPlayListModal(sender as Card);
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
        private static void OpenPlayListModal(Card selectedCard)
        {
            if (selectedCard is not null)
            {
                var modal = new PlayListViewer(selectedCard.Uid)
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






using HandyControl.Controls;
using MaterialDesignThemes.Wpf;
using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Helpers;
using MosqueMateV2.Pages;
using MosqueMateV2.Resources;
using MosqueMateV2.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Card = HandyControl.Controls.Card;
using Clipboard = System.Windows.Clipboard;

namespace MosqueMateV2.Extensions
{
    public static class ControlExtenstion
    {
        public static void AddImageToButton(this ButtonBase button, string uriPath)
        {
            if (button == null || string.IsNullOrEmpty(uriPath))
                return;

            try
            {
                // Create an Image control
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(uriPath, UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.UniformToFill,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 24,
                    Height = 24,
                };
                button.Background = null;
                button.BorderBrush = null;
                button.Content = image;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding image to button: {ex.Message}");
            }
        }
        public static void AddToolTipToButton(this ButtonBase button, string tooltip)
        {
            if (button == null || string.IsNullOrEmpty(tooltip))
                return;

            try
            {
                button.ToolTip = tooltip;
            }
            catch (Exception ex)
            {
            }
        }



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
        /// <param name="PaddingLeftTxt">Determine Padding left Textblock of card</param>
        /// <param name="PaddingRightTxt">Determine Padding right Textblock of card</param>
        /// <param name="PaddingTopTxt">Determine Padding top Textblock of card</param>
        /// <param name="PaddingBottomTxt">Determine Padding bottom Textblock of card</param>
        /// <param name="flowDirection">Determine Flow Direction of Card</param>
        public static void GenerateCards<T>(
        this Grid grid,
        List<T> data,
        Func<T, string> getName,
        Func<T, int> getId,
        PagesTypesEnum serviceType,
        Func<T, string> UidCard = null,
        int CardWidth = 250,
        int CardHeight = 150,
        int PaddingLeftTxt = 0,
        int PaddingRightTxt = 0,
        int PaddingTopTxt = 0,
        int PaddingBottomTxt = 0,
        int FontSize = 10,
        Func<T, string> HeaderTxt = null,
        FlowDirection flowDirection = FlowDirection.LeftToRight,
        TextAlignment textAlignment = TextAlignment.Center,
        TextWrapping textWrapping = TextWrapping.NoWrap
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
                    Uid = UidCard is not null ? UidCard(item) : "",
                    Width = CardWidth,
                    Height = CardHeight,
                    Margin = new Thickness(10),
                    Background = ColorHelper.CreateGradientBackgroundRgb(26, 132, 184, 53, 76, 124),
                    Padding = new Thickness(16),
                    Foreground = new SolidColorBrush(Colors.White),
                    Cursor = Cursors.Hand,
                    FlowDirection = flowDirection,
                    BorderBrush = null,
                };
                card.MouseLeftButtonDown += (sender, e) => Card_MouseLeftButtonDown(sender, e, serviceType);

                StackPanel contentPanel = new();
                TextBlock header = new();
                if (HeaderTxt is not null)
                {
                    header = new()
                    {
                        Text = HeaderTxt(item),
                        FontSize = FontSize,
                        FontWeight = FontWeights.UltraBold,
                        Margin = new Thickness(0, 0, 0, 8),
                        TextWrapping = textWrapping,
                        TextAlignment = textAlignment,
                        Padding = new Thickness(PaddingLeftTxt,
                                      PaddingTopTxt,
                                      PaddingRightTxt,
                                      PaddingBottomTxt),
                        LineHeight = 30
                    };
                }
                TextBlock title = new()
                {
                    Text = getName(item),
                    FontSize = 20,
                    FontWeight = FontWeights.UltraBold,
                    Margin = new Thickness(0, 0, 0, 8),
                    TextWrapping = TextWrapping.WrapWithOverflow,
                    TextAlignment = textAlignment,
                    Padding = new Thickness(PaddingLeftTxt,
                                            PaddingTopTxt,
                                            PaddingRightTxt,
                                            PaddingBottomTxt),
                    LineHeight = 30
                };


                if (HeaderTxt is not null)
                    contentPanel.Children.Add(header);
                contentPanel.Children.Add(title);
                card.Content = contentPanel;
                cardPanel.Children.Add(card);
            }

            grid.Children.Add(cardPanel);
        }


        public static Card GenerateCardForHadith(
                string title1 = "",
                string title2 = "",
                string title3 = "",
                string title4 = ""
                 )
        {

            var card = new Card
            {

                BorderBrush = null,
                Background = ColorHelper.CreateGradientBackgroundCode("#1E201E", "#021526"),
                Foreground = Brushes.PapayaWhip,
                //Width = 1000,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,

            };
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Star) });

            // Create TextBlocks
            var title1Txt = new TextBlock
            {
                Name = "title1Txt",
                Text = title1,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Margin = new Thickness(0, 10, 0, 99),
                FontSize = 20,
                Foreground = Brushes.Wheat,
                Background = Brushes.Transparent,
            };
            Grid.SetRow(title1Txt, 0);

            var title2Txt = new TextBlock
            {
                Name = "title2Txt",
                Text = title2,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Margin = new Thickness(0, 80, 0, 99),
                FontSize = 20,
                Foreground = Brushes.Wheat,
                FlowDirection = FlowDirection.RightToLeft,
                Background = Brushes.Transparent,
            };
            Grid.SetRow(title2Txt, 0);
            var title3Txt = new TextBlock
            {
                Name = "title3Txt",
                Text = title3,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Margin = new Thickness(0, 5, 0, 99),
                FontSize = 20,
                FontWeight = FontWeights.UltraBold,
                Foreground = Brushes.White,
                Background = Brushes.Transparent,
                LineHeight = 40,
            };
            Grid.SetRow(title3Txt, 1);

            var title4Txt = new TextBlock
            {
                Name = "title4Txt",
                Text = title4,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                FontSize = 20,
                Background = Brushes.Transparent,
                Foreground = Brushes.White,
                LineHeight = 40,

            };
            Grid.SetRow(title4Txt, 2);

            var copyToClipboard = new Button
            {
                Name = "copyToClipboard",
                ToolTip = "Copy Hadith",
                Background = Brushes.Transparent,
                Margin = new Thickness(20),
                HorizontalAlignment = HorizontalAlignment.Right,
                Height = 35,
                Width = 49,
                Content = new PackIcon
                {
                    Kind = PackIconKind.ContentCopy,
                    Foreground = Brushes.Wheat,
                }
            };
            var text3_and_4 = title3Txt.Text + "\n\n" + title4Txt.Text;
            copyToClipboard.Click += (sender, e) => CopyToClipboard_Click(sender, e, text3_and_4);
            Grid.SetRow(copyToClipboard, 3);

            var shareButton = new Button
            {
                Name = "Share",
                ToolTip = "Copy Hadith",
                Background = Brushes.Transparent,
                Margin = new Thickness(30, 0, 0, 0),
                Height = 35,
                Width = 49,
                Content = new PackIcon
                {
                    Kind = PackIconKind.ShareOutline,
                    Foreground = Brushes.Wheat,
                }
            };
            shareButton.Click += (sender, e) => ShareButton_Click(sender, e, text3_and_4);

            Grid.SetRow(shareButton, 3);
            grid.Children.Add(title1Txt);
            grid.Children.Add(title2Txt);
            grid.Children.Add(title3Txt);
            grid.Children.Add(title4Txt);
            grid.Children.Add(copyToClipboard);
            grid.Children.Add(shareButton);

            // Add Grid to Card
            card.Content = grid;
            return card;
        }


        public static void GenerateCardsForHadith(this Grid grid, List<Data> data)
        {
            if (data.Any())
            {
                StackPanel cardPanel = new()
                {
                    Margin = new Thickness(30),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,

                };
                TextBlock textBlock = new()
                {
                    Text = App.LocalizationService[AppLocalization.HadithCount] + " : " + data.Count,
                    Margin = new Thickness(25, 0, 0, 0)
                };
                cardPanel.Children.Add(textBlock);
                foreach (var item in data)
                {
                    var head1 = $"Chapter ({item.chapterId}) : " + item.headingEnglish;
                    var head2 = $"({item.chapterId}) : " + item.headingArabic;
                    var card = GenerateCardForHadith(
                           title1: head1,
                           title2: head2,
                           title3: item.hadithArabic,
                           title4: item.hadithEnglish
                           );
                    cardPanel.Children.Add(card);
                }
                grid.Children.Add(cardPanel);
            }
        }

        private static Card GenerateCardForAllahNames(
        string title1 = "",
        string title3 = ""
         )
        {
            // Create the Card
            var card = new Card
            {

                BorderBrush = null,
                Background = ColorHelper.CreateGradientBackgroundCode("#1E201E", "#021526"),
                Foreground = Brushes.PapayaWhip,
                Height = 400,
                Width = 550,
                Margin = new Thickness(10),

            };

            // Create the Grid
            var grid = new Grid();

            // Define RowDefinitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250, GridUnitType.Star) });

            // Create TextBlocks
            var title1Txt = new TextBlock
            {
                Name = "title1Txt",
                Text = title1,
                Padding = new Thickness(20),
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                Margin = new Thickness(0, 0, 0, 99),
                FontSize = 20,
                Foreground = Brushes.Wheat,
            };
            Grid.SetRow(title1Txt, 0);
            var title3Txt = new TextBlock
            {
                Name = "title3Txt",
                Text = title3,
                TextAlignment = TextAlignment.Center,
                TextWrapping = TextWrapping.WrapWithOverflow,
                FontSize = 20,
                Margin = new Thickness(0, -100, 0, 0),
                FontWeight = FontWeights.UltraBold,
                LineHeight = 40,
                Foreground = Brushes.White,

            };
            Grid.SetRow(title3Txt, 1);

            // Create Buttons
            var copyToClipboard = new Button
            {
                Name = "copyToClipboard",
                ToolTip = "Copy Hadith",
                Background = Brushes.Transparent,
                Margin = new Thickness(470, 90, 0, 0),
                Height = 35,
                Width = 49,
                Content = new PackIcon
                {
                    Kind = PackIconKind.ContentCopy,
                    Foreground = Brushes.Wheat,
                }
            };
            var text3_and_4 = title3Txt.Text;
            copyToClipboard.Click += (sender, e) => CopyToClipboard_Click(sender, e, text3_and_4);
            Grid.SetRow(copyToClipboard, 3);

            var shareButton = new Button
            {
                Name = "Share",
                ToolTip = "Copy Hadith",
                Background = Brushes.Transparent,
                Margin = new Thickness(20, 80, 0, 0),
                Height = 35,
                Width = 49,
                Content = new PackIcon
                {
                    Kind = PackIconKind.ShareOutline,
                    Foreground = Brushes.Wheat,
                }
            };
            shareButton.Click += (sender, e) => ShareButton_Click(sender, e, text3_and_4);

            Grid.SetRow(shareButton, 3);
            grid.Children.Add(title1Txt);
            grid.Children.Add(title3Txt);
            grid.Children.Add(copyToClipboard);
            grid.Children.Add(shareButton);

            // Add Grid to Card
            card.Content = grid;
            return card;
        }
        public static LoadingLine GenerateLineLoading()
        {
            var loadingLine = new LoadingLine
            {
                Margin = new Thickness(100, 0, 0, 0),
                Foreground = new SolidColorBrush(Colors.Blue),
                Style = (Style)Application.Current.Resources["LoadingLineLarge"],
                Visibility = Visibility.Collapsed,
            };
            return loadingLine;
        }
        private static void CopyToClipboard_Click(object sender, RoutedEventArgs e, string value)
        {
            Clipboard.SetText(value);
            ToastNotificationsHelper.SendNotification(
                title: "Clipboard",
                message: "Text Copy Sucessfully",
                duration: new TimeSpan(0, 0, 5),
                type: Notification.Wpf.NotificationType.Success
                );
        }
        private static void ShareButton_Click(object sender, RoutedEventArgs e, string value)
        {
            ShareHelper.ShareText("Share Text", "Share text using Windows Share functionality.", value);
        }
        public static void GenerateCardsForAllahNames(this Grid grid, List<DTOAllahNames> data)
        {
            if (data.Any())
            {
                WrapPanel cardPanel = new()
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Margin = new Thickness(220, 30, 0, 0),
                };
                foreach (var item in data)
                {
                    var card = GenerateCardForAllahNames(
                           title1: item.name,
                           title3: item.text
                           );
                    cardPanel.Children.Add(card);
                }
                grid.Children.Add(cardPanel);
            }
        }


        private static void Card_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, PagesTypesEnum service)
        {
            switch (service)
            {
                case PagesTypesEnum.Quran:
                    OpenQuranModal(sender as Card);
                    break;
                case PagesTypesEnum.Adhkar:
                    OpenAdhkarModal(sender as Card);
                    break;
                case PagesTypesEnum.Hadith:
                    OpenHadithChapters(sender as Card);
                    break;
                case PagesTypesEnum.HadithChapter:
                    OpenHadithInfo(sender as Card);
                    break;
                case PagesTypesEnum.YoutubeViewerPage:
                    OpenYoutubeViewer(sender as Card);
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

        #region Modal
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
        private static void OpenAdhkarModal(Card selectedCard)
        {
            if (selectedCard is not null)
            {
                var id = NumberHelper.IsTextHasDigit(selectedCard.Name) ?
                    NumberHelper.GetTextFromDigit(selectedCard.Name) : 1;
                new AdhkarModalPopup(id).ShowModal();
            }
        }
        private static void OpenHadithChapters(Card selectedCard)
        {
            if (selectedCard is not null)
            {
                var index = NumberHelper.IsTextHasDigit(selectedCard.Name) ?
                    NumberHelper.GetTextFromDigit(selectedCard.Name) : 1;
                var window = Application.Current.MainWindow;
                var frame = window.FindName("MainFrame") as Frame;
                if (frame is not null)
                {
                    var uId = selectedCard.Uid;
                    frame?.Navigate(new HadithChapters(uId));
                }

            }
        }
        private static void OpenHadithInfo(Card selectedCard)
        {
            if (selectedCard is not null)
            {
                var index = NumberHelper.IsTextHasDigit(selectedCard.Name) ?
                    NumberHelper.GetTextFromDigit(selectedCard.Name) : 1;
                var window = Application.Current.MainWindow;
                var frame = window.FindName("MainFrame") as Frame;
                if (frame is not null)
                {
                    var uId = selectedCard.Uid;
                    frame?.Navigate(new HadithInfo(index));
                }

            }
        }
        private static void OpenYoutubeViewer(Card selectedCard)
        {
            if (selectedCard is not null)
            {
                var window = Application.Current.MainWindow;
                var frame = window.FindName("MainFrame") as Frame;
                if (frame is not null)
                {
                    var uId = selectedCard.Uid;
                    frame?.Navigate(new YoutubeViewerPage(uId));
                }

            }
        }

        #endregion
    }

    public class ControlBinding<T>
    {
        public Point? ControlPosition { get; set; }
        public T? SelectedElement { get; set; }
    }
}

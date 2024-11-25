using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

                button.Content = image;
                button.Background = null;
                button.BorderBrush = null;
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., invalid URI)
                MessageBox.Show($"Error adding image to button: {ex.Message}");
            }
        }

        //public static void ChangeWindowFont(this Window window, byte[] fontData)
        //{
        //    using (var stream = new MemoryStream(fontData))
        //    {
        //        // Create a PrivateFontCollection to load the font
        //        PrivateFontCollection privateFonts = new PrivateFontCollection();
        //        privateFonts.AddMemoryFont(stream); // Add font from stream

        //        // Get the font family name
        //        string fontFamilyName = privateFonts.Families[0].Name;

        //        window.FontFamily = privateFonts
        //    }
        //}
    }
}

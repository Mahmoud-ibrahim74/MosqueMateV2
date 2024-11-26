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
                button.Background = null;
                button.BorderBrush = null;
                button.Content = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding image to button: {ex.Message}");
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
    }
}

using System.IO;
using System.Windows.Media.Imaging;

namespace MosqueMateV2.Helpers
{
    public class ImageHelper
    {
        public static BitmapImage LoadGifImage()
        {     
            // Load the GIF
            BitmapImage gifBitmap = new();
            gifBitmap.BeginInit();
            gifBitmap.UriSource = new Uri("pack://application:,,,/Assets/loading.gif", UriKind.RelativeOrAbsolute); // Use your GIF path
            gifBitmap.CacheOption = BitmapCacheOption.OnLoad; // Load immediately
            gifBitmap.EndInit();
            gifBitmap.Freeze(); 
            return gifBitmap;
        }
    }
}

using System.IO;
using System.Windows.Media.Imaging;

namespace MosqueMateV2.Helpers
{
    public class ImageHelper
    {
        public BitmapImage ConvertBytesToImage(byte[] imageData)
        {
            BitmapImage bitmap = new BitmapImage();

            using (MemoryStream stream = new MemoryStream(imageData))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad; // Load the image into memory
                bitmap.StreamSource = stream;
                bitmap.EndInit();
            }

            bitmap.Freeze(); // Freeze to make it thread-safe and immutable
            return bitmap;
        }
    }
}

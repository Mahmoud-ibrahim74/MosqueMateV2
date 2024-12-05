using System.Drawing;
using System.IO;
using System.Windows;
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
        public static BitmapImage ConvertBitmapToBitmapImage(Bitmap bitmap)
        {
            using MemoryStream memoryStream = new();
            // Save the Bitmap to the MemoryStream as PNG or JPEG (you can choose the format)
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

            // Reset the position of the stream before loading into BitmapImage
            memoryStream.Position = 0;

            // Create a BitmapImage and load it from the MemoryStream
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();

            return bitmapImage;
        }
        public static BitmapImage ConvertBytesToImage(byte[] imageData)
        {
            BitmapImage bitmap = new();

            using (MemoryStream stream = new(imageData))
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

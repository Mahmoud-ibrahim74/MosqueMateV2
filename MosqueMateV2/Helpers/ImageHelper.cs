﻿using System.Drawing;
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
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Position = 0;
            BitmapImage bitmapImage = new();
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
        public static BitmapFrame ConvertBytesToBitmapFrame(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using var memoryStream = new MemoryStream(byteArray);
            memoryStream.Seek(0, SeekOrigin.Begin);
            BitmapFrame bitmapFrame = BitmapFrame.Create(
                memoryStream,
                BitmapCreateOptions.PreservePixelFormat,
                BitmapCacheOption.OnLoad
            );

            return bitmapFrame;
        }
    }
}

using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;

namespace MosqueMateV2.Helpers
{
    public class ColorHelper
    {
        public static LinearGradientBrush CreateGradientBackgroundRgb(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
        {
            return new LinearGradientBrush
            {
                GradientStops =
                        [
                            new GradientStop(Color.FromRgb(r1,g1,b1), 0.0), // 26,132,184,53,76,124
                            new GradientStop(Color.FromRgb(r2,g2,b2), 1.0)  // 53,76,124
                        ],
                StartPoint = new Point(0, 0),  // Top-left corner
                EndPoint = new Point(1, 1)    // Bottom-right corner
            };
        }    
        public static LinearGradientBrush CreateGradientBackgroundCode(string colorCode1, string colorCode2)
        {
            if (colorCode1.Length != 7 || colorCode2.Length != 7 ||
                    !colorCode1.StartsWith("#") || !colorCode2.StartsWith("#"))        
                return new LinearGradientBrush();
           
            Color color1 = (Color)ColorConverter.ConvertFromString(colorCode1);
            Color color2 = (Color)ColorConverter.ConvertFromString(colorCode2);
            return new LinearGradientBrush
            {
                GradientStops =
                        [
                            new GradientStop(color1, 0.0),
                            new GradientStop(color2, 1.0)  
                        ],
                StartPoint = new Point(0, 0), 
                EndPoint = new Point(1, 1)   
            };
        }
    }
}

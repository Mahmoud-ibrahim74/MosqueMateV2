using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MosqueMateV2.Helpers
{
    public  class AppHelper
    {
        public static string ChangeAppFont(byte[] fontData)
        {
            if (fontData.Length == 0)
                return null;

            string tempFontPath = Path.Combine(Path.GetTempPath(), "tempFont.ttf");
            File.WriteAllBytes(tempFontPath, fontData);
            var fontFamily = new FontFamily(new Uri($"file:///{tempFontPath}"), "./#CustomFont");
            // Set a global style in Application.Resources
            Application.Current.Resources.Add(typeof(TextElement), new Style
            {
                Setters =
                    {
                        new Setter(TextElement.FontFamilyProperty, fontFamily)
                    }
            });
            return tempFontPath;
        }
    }
}

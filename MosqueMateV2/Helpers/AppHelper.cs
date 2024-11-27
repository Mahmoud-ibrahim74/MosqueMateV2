using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace MosqueMateV2.Helpers
{
    public class AppHelper
    {
        public static string ChangeAppFont(byte[] fontData)
        {
            if (fontData.Length == 0)
                return null;

            string tempFontPath = Path.Combine(Path.GetTempPath(), "tempFont.ttf");
            if (File.Exists(tempFontPath))
                File.Delete(tempFontPath);
            File.WriteAllBytes(tempFontPath, fontData);
            var fontFamily = new FontFamily(new Uri($"file:///{tempFontPath}"), "./#CustomFont");
            var fontStyle = new Style(typeof(TextElement));
            fontStyle.Setters.Add(new Setter(TextElement.FontFamilyProperty, fontFamily));

            var controlStyle = new Style(typeof(Control));
            controlStyle.Setters.Add(new Setter(Control.FontFamilyProperty, fontFamily));

            var textBlockStyle = new Style(typeof(TextBlock));
            textBlockStyle.Setters.Add(new Setter(TextBlock.FontFamilyProperty, fontFamily));

            Application.Current.Resources.Add(typeof(TextElement), fontStyle);
            Application.Current.Resources.Add(typeof(Control), controlStyle);
            Application.Current.Resources.Add(typeof(TextBlock), textBlockStyle);
            return tempFontPath;
        }
    }
}

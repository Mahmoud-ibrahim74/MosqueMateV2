
using System.Diagnostics;
using System.IO;
using System.Windows;
namespace MosqueMateV2.Helpers
{
    public class ShareHelper
    {
        public static void ShareText(string textToShare)
        {

            try
            {
                // Copy text to clipboard
                Clipboard.SetText(textToShare);

                // Open default email client
                string mailto = $"mailto:?body={Uri.EscapeDataString(textToShare)}";
                Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
            }
            catch (Exception)
            {
            }
        }

    }
}

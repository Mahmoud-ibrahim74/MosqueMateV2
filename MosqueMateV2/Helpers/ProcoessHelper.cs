using System.Diagnostics;

namespace MosqueMateV2.Helpers
{
    public class ProcoessHelper
    {
        public static void OpenAppLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
            }
        }
    }
}

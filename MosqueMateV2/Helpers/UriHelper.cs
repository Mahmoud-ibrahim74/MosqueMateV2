using System.IO;

namespace MosqueMateV2.Helpers
{
    public class UriHelper
    {
        public static string CreateBlobUrl(MemoryStream memoryStream)
        {
            // Create a Blob URL to display the PDF in WebView2
            string base64String = Convert.ToBase64String(memoryStream.ToArray());
            return $"data:application/pdf;base64,{base64String}";
        }
        public static Uri CreateBlobUri(MemoryStream memoryStream)
        {
            // Create a Blob URL to display the PDF in WebView2
            string base64String = Convert.ToBase64String(memoryStream.ToArray());
            return new Uri($"data:application/pdf;base64,{base64String}");
        }
    }
}

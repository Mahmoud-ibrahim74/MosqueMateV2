using MosqueMateV2.Resources;
using System.IO;

namespace MosqueMateV2.Helpers
{
    public class FileHelper
    {
        public static async Task<string> WritePdfToTempAsync(byte[] file)
        {
            if (file.Length == 0)
                return null;

            var filePath = Path.Combine(AppLocalization.AppTempPath, "output.pdf");
            await File.WriteAllBytesAsync(filePath, file);
            return filePath;
        }
    }
}

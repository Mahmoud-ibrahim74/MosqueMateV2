using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Resources
{
    public class AdhkarResource
    {
        static string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "AdhakrAudio");

        public static string[] GetAdhakarFileNames()
        {
            if (Directory.Exists(folderPath))
            {
                return Directory.GetFiles(folderPath);
            }
            return [];
        }

        public static byte[] GetZekrBytes(string zekrName)
        {
            var fullPath = Path.Combine(folderPath, zekrName);
            if (File.Exists(fullPath))
                return File.ReadAllBytes(fullPath);
            return [];
        }
    }
}

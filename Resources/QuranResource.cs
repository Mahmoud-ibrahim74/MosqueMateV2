using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Resources
{
    public class QuranResource
    {
        string direPath { get; set; }
        string[] dirContent = [];
        public QuranResource()
        {
            string outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string resourcesFolder = Path.Combine(outputDirectory, "Resources");
            direPath = resourcesFolder;
            if (Directory.Exists(direPath))
                dirContent = Directory.GetFiles(direPath);

        }
        public Byte[] GetPageContent(string page_name)
        {
            var filePath = Path.Combine(direPath, page_name);
            if (File.Exists(filePath))
                return File.ReadAllBytes(filePath);

            return [];
        }


    }
}

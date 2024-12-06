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
        public Byte[] GetPageContent(int pageIndex)
        {
            var formattedNumber = pageIndex.ToString("D3");
            var fileName = $"quran_hafs_m_Page_{formattedNumber}.png";
            var filePath = Path.Combine(direPath, fileName);
            if (File.Exists(filePath))
                return File.ReadAllBytes(filePath);

            return [];
        }


    }
}

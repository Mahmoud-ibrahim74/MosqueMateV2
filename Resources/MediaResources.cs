using Newtonsoft.Json.Linq;

namespace Resources
{
    public class MediaResources
    {
        private readonly static string AppResourcePath = Path.Combine(Environment.CurrentDirectory, "AppResources");
        public string[] AdhanNames { get; set; } = [];
        public static Dictionary<string, byte[]> GetAdhanFiles()
        {
            var dirPath = Path.Combine(AppResourcePath, "Adhan");
            if (!Directory.Exists(dirPath))
                return [];


            return Directory.GetFiles(dirPath)
                            .ToDictionary(file => Path.GetFileName(file),
                                          file => File.ReadAllBytes(file));
        }
        public static byte[] SearchOnAdhanFiles(string word)
        {
            var dirPath = Path.Combine(AppResourcePath, "Adhan");
            if (!Directory.Exists(dirPath))
                return [];

            var file = Directory.GetFiles(dirPath).
                Where(x => x.Contains(word)).
                Select(file => File.ReadAllBytes(file)).
                FirstOrDefault() ?? [];

            return file;                     
        }
        public static byte[] GetFontToByteArray()
        {
            var fontPath = Path.Combine(AppResourcePath, "VIP-Hakm-Regular-2016.ttf");
            if (!File.Exists(fontPath))
                return [];
         
            return File.ReadAllBytes(fontPath);
        }
    }
}

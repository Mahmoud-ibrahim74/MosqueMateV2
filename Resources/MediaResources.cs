using Newtonsoft.Json.Linq;

namespace Resources
{
    public class MediaResources
    {
        
        public string[] AdhanNames { get; set; } = [];
        public MediaResources()
        {
           
        }
        public static Dictionary<string, byte[]> GetAdhanFiles()
        {
            string dirPath = Path.Combine(Environment.CurrentDirectory, "AppResources", "Adhan");
            if (!Directory.Exists(dirPath))
            {
                throw new DirectoryNotFoundException($"The directory '{dirPath}' does not exist.");
            }

            return Directory.GetFiles(dirPath)
                            .ToDictionary(file => Path.GetFileName(file),
                                          file => File.ReadAllBytes(file));
        }
    }
}

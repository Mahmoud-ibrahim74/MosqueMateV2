using MosqueMateV2.Resources;
using MosqueMateV2.Service.IServices;

namespace MosqueMateV2.Service.Services
{
    public class FileServices : IFileServices
    {
        public string AppTempPath { get; set; }

        public FileServices()
        {
            this.AppTempPath = Path.Combine(Path.GetTempPath(), SD.Localization.AppAssemblyName);

            if (!Directory.Exists(this.AppTempPath))
                Directory.CreateDirectory(this.AppTempPath);
        }
        public string CombinePathWithTemp(params string[] pathes)
        {
            if (!pathes.Any())
                return AppTempPath;

            return Path.Combine(AppTempPath, Path.Combine(pathes));
        }
    }
}

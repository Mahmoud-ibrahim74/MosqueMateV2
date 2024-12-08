using MosqueMateV2.Resources;
using MosqueMateV2.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Service.Services
{
    public class FileServices : IFileServices
    {
        public string AppTempPath { get; set; }

        public FileServices()
        {
            this.AppTempPath = Path.Combine(Path.GetTempPath(), AppLocalization.AppAssemblyName);

            if (!Directory.Exists(this.AppTempPath))
                Directory.CreateDirectory(this.AppTempPath);
        }
        public string CombinePathWithTemp(params string[] pathes)
        {
            if (!pathes.Any())
                return AppTempPath;

            return Path.Combine(AppTempPath, Path.Combine(pathes));
        }

        ~FileServices()
        {
            if (Directory.Exists(this.AppTempPath))
            {
                Directory.Delete(this.AppTempPath);
                Console.WriteLine("temp is cleaned");
            }
        }
    }
}

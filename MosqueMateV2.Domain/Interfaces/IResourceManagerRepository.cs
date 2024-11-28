using MosqueMateV2.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface IResourceManagerRepository : IDisposable
    {
        public List<ResourceEntry> GetAllResourcesInfoFromResx();
        public byte[] GetResourceByte(string key);
        public string GetStringResource(string key);
        public UnmanagedMemoryStream GetStreamResource(string key);
    }
}

using MosqueMateV2.Resources;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface IResourceManagerRepository : IDisposable
    {
        public Task<List<ResourceEntry>> GetAllResourcesInfoFromResxAsync();
        public Task<List<ResourceEntry>> GetAllResourcesFromResxAsync(string _prefixName);

        public byte[] GetResourceByte(string key);
        public string GetStringResource(string key);
        public UnmanagedMemoryStream GetStreamResource(string key);
        public void AddResourceToFile(string filePath);

    }
}

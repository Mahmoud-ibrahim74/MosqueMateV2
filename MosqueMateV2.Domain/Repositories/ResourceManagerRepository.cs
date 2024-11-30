using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace MosqueMateV2.Domain.Repositories
{
    public class ResourceManagerRepository : IResourceManagerRepository
    {
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
        }
        ResourceManager resourceManager;
        private CultureInfo resourceCulture;

        public ResourceManagerRepository(ResourceTypeEnum resourceType)
        {
            var projName = "MosqueMateV2.Resources";
            resourceManager = resourceType switch
            {
                ResourceTypeEnum.FileResources => new ResourceManager($"{projName}.{ResourceTypeEnum.FileResources}", typeof(FileResources).Assembly),
                ResourceTypeEnum.MediaResources => new ResourceManager($"{projName}.{ResourceTypeEnum.MediaResources}", typeof(MediaResources).Assembly),
                ResourceTypeEnum.FontResources => new ResourceManager($"{projName}.{ResourceTypeEnum.FontResources}", typeof(FontResources).Assembly),
                _ => new ResourceManager($"{projName}.{ResourceTypeEnum.MediaResources}", typeof(MediaResources).Assembly),
            };
        }
        public Task<List<ResourceEntry>> GetAllResourcesInfoFromResxAsync()
        {
            List<ResourceEntry> resources = [];
            if (resourceManager is null)
                return Task.FromResult(resources);

            resources = resourceManager
                   .GetResourceSet(CultureInfo.InvariantCulture, true, true)?
                   .Cast<DictionaryEntry>()
                   .Select(entry =>
                       {
                           return new ResourceEntry
                           {
                               Name = entry.Key.ToString(),
                           };
                       }).ToList() ?? [];
            return Task.FromResult(resources);
        }
        public byte[] GetResourceByte(string key)
        {
            try
            {
                var obj = resourceManager.GetObject(key, Culture);
                if (obj is null)
                    return [];

                return (byte[])obj;
            }
            catch (Exception)
            {
                return [];
            }

        }
        public string GetStringResource(string key)
        {
            try
            {
                var obj = resourceManager.GetString(key, Culture);

                if (obj is null)
                    return string.Empty;
                return obj;

            }
            catch (Exception)
            {

                return string.Empty;

            }
        }
        public UnmanagedMemoryStream GetStreamResource(string key)
        {
            try
            {
                var obj = resourceManager.GetStream(key, Culture);

                if (obj is null)
                    return null;

                return obj;

            }
            catch (Exception)
            {
                return null;
            }
        }
        public void Dispose() => resourceManager.ReleaseAllResources();
    }
}

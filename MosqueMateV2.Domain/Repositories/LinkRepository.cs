using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Repositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly List<DTOQuranLinks> _links;

        public LinkRepository()
        {
            if (FileResources.countries.Length == 0)
                return;

            string jsonContent = Encoding.UTF8.GetString(FileResources.quran_link);
            _links = JsonConvert.DeserializeObject<List<DTOQuranLinks>>(jsonContent) ?? [];
        }
        public Task<List<DTOQuranLinks>> GetAllLinks()
        {
            return Task.FromResult(_links ?? []);
        }
        public DTOQuranLinks GetLinkByName(string name)
        {
            return _links.FirstOrDefault(x => x.name.Contains(name));
        }
    }
}

using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
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
            _links = JsonConvert.DeserializeObject<List<DTOQuranLinks>>(jsonContent) ?? new List<DTOQuranLinks>();
        }
        public Task<List<DTOQuranLinks>> GetAllLinks()
        {
            return Task.FromResult(_links ?? new List<DTOQuranLinks>());
        }
        public DTOQuranLinks GetLinkByName(string name)
        {

            return _links.FirstOrDefault(x => x.name == name) ??
            new DTOQuranLinks
            {
                name = SD.Localization.DefaultSura,
                url1 = "https://www.youtube.com/watch?v=SxzUeUdi5hI&list=PLdjxZcgE9WhA-0aup6tYg7soQRNhxOSHr",
            };
        }
        public string ModifyOneLink(List<string> urls)
        {
            for (int i = 0; i < _links.Count; i++)
            {
                _links[i].url1 = urls[i];
            }
            var serialized = JsonConvert.SerializeObject(_links);
            return serialized;
        }
    }
}

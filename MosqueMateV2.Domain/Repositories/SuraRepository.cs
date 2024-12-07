using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Repositories
{
    public class SuraRepository: ISuraRepository
    {
        private readonly List<DTOSuraNames> _suraObj;

        public SuraRepository()
        {
            if (FileResources.countries.Length == 0)
                return;

            string jsonContent = Encoding.UTF8.GetString(FileResources.surahNames);
            _suraObj = JsonConvert.DeserializeObject<List<DTOSuraNames>>(jsonContent) ?? [];
        }
        public Task<List<DTOSuraNames>> GetAllSuraNames()
        {
            return Task.FromResult(_suraObj ?? []);
        }
        public DTOSuraNames GetSuranByName(string name)
        {
            return _suraObj.FirstOrDefault(x => x.name.Contains(name));
        }
        public DTOSuraNames GetSuraById(int id)
        {
            return _suraObj.FirstOrDefault(x => x.pageIndex == id);

        }
    }
}

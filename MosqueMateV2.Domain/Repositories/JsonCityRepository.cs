using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Repositories
{
    public class JsonCityRepository : IJsonCityRepository
    {
        private readonly List<DTOCity> _cityObj = [];

        public JsonCityRepository()
        {
            if (FileResources.countries.Length == 0)
                return;

            string jsonContent = Encoding.UTF8.GetString(FileResources.cities);
            _cityObj = JsonConvert.DeserializeObject<List<DTOCity>>(jsonContent);
        }

        public void Dispose()
        {

        }

        public Task<List<string>> GetAllCities()
        {
            var res = _cityObj.
                        Select(x => x.city)
                        .ToList();

            return Task.FromResult(res);
        }
        public List<string> SearchOnCountry(string word)
        {
            var res = _cityObj.
                Where(x => x.country.Contains(word)).
                Select(x => x.city)
                .ToList();
            return res ?? [];
        }
    }
}

using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Repositories
{
    public class JsonCountryRepository : IJsonCountryRepository
    {
        private readonly List<DTOCountry> _countryObj = [];

        public JsonCountryRepository()
        {
            if (FileResources.countries.Length == 0)
                return;

            string jsonContent = Encoding.UTF8.GetString(FileResources.countries);
             _countryObj = JsonConvert.DeserializeObject<List<DTOCountry>>(jsonContent);
        }

        public void Dispose()
        {
           
        }

        public Task<List<string>> GetAllCountiresAsync()
        {
            var res = _countryObj.
                        Select(x => x.english_name)
                        .ToList();

            return Task.FromResult(res);
        }
        public string SearchOnCountry(string word)
        {
            var res = _countryObj.
                Where(x => x.arabic_name.Contains(word) ||
                x.english_name.Contains(word)).
                Select(x => x.english_name)
                .FirstOrDefault();
            return res ?? string.Empty;
        }
    }
}

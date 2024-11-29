namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonCityRepository : IDisposable
    {
        public Task<List<string>> GetAllCities();
        public List<string> SearchOnCountry(string word);
    }
}

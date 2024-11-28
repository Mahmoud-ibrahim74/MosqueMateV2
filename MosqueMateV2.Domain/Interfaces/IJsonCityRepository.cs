namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonCityRepository : IDisposable
    {
        public Task<List<string>> GetAllCountiresLocalization();
        public string SearchOnCountry(string word);
    }
}

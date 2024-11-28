namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonCountryRepository:IDisposable
    {
        public Task<List<string>> GetAllCountiresLocalization();
        public string SearchOnCountry(string word);
    }
}

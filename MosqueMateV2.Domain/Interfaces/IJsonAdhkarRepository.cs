namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonAdhkarRepository:IDisposable
    {
        public Task<List<string>> GetAllAdhkarsAsync();
    }
}

using MosqueMateV2.Domain.DTOs;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonAdhkarRepository:IDisposable
    {
        public Task<List<DTOAdhkar>> GetAllAdhkarsAsync();
        public Task<int> GetAllAdhkarsCountAsync();
    }
}

using MosqueMateV2.Domain.DTOs;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonAdhkarRepository:IDisposable
    {
        public Task<List<DTOAdhkar>> GetAllAdhkarsAsync();
        public Task<DTOAdhkar> GetZekrByIdAsync(int id);
        public Task<int> GetAllAdhkarsCountAsync();
    }
}

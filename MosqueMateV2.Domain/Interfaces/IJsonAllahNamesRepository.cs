using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface IJsonAllahNamesRepository : IDisposable
    {
        public Task<List<DTOAllahNames>> GetAllNames();
        public Task<DTOAllahNames> GetNameById(int id);
        public Task<int> GetAllNamesCountAsync();
    }
}

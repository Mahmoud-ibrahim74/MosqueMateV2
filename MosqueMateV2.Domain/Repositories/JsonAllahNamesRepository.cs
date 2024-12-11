using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Repositories
{
    public class JsonAllahNamesRepository : IJsonAllahNamesRepository
    {
        private readonly List<DTOAllahNames> _namesObj = [];

        public JsonAllahNamesRepository()
        {
            if (FileResources.adhkar.Length == 0)
                return;

            string jsonContent = Encoding.UTF8.GetString(FileResources.Names_Of_Allah);
            _namesObj = JsonConvert.DeserializeObject<List<DTOAllahNames>>(jsonContent);
        }

        public Task<List<DTOAllahNames>> GetAllNames()
        {
            return Task.FromResult(_namesObj);
        }
        public Task<DTOAllahNames> GetNameById(int id)
        {
            var res = _namesObj.FirstOrDefault(x => x.id == id) ?? new();
            return Task.FromResult(res);
        }


        public void Dispose()
        {
            _namesObj.Clear();
        }

        public Task<int> GetAllNamesCountAsync()
        {
            return Task.FromResult(_namesObj.Count);
        }
    }
}

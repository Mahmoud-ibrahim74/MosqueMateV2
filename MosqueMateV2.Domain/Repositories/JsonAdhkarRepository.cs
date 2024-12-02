using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Repositories
{
    public class JsonAdhkarRepository : IJsonAdhkarRepository
    {
        private readonly List<DTOAdhkar> _adhkarObj = [];
        private bool disposedValue;

        public JsonAdhkarRepository()
        {
            if (FileResources.adhkar.Length == 0)
                return;

            string jsonContent = Encoding.UTF8.GetString(FileResources.adhkar);
            _adhkarObj = JsonConvert.DeserializeObject<List<DTOAdhkar>>(jsonContent);
        }

        public Task<List<string>> GetAllAdhkarsAsync()
        {
            var res = _adhkarObj.
                        Select(x => x.category)
                        .ToList();

            return Task.FromResult(res);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~JsonAdhkarRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            _adhkarObj.Clear();
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

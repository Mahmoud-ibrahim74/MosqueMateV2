using MosqueMateV2.Domain.DTOs;
using MosqueMateV2.Domain.Enums;
using MosqueMateV2.Domain.Interfaces;
using MosqueMateV2.Resources;
using Newtonsoft.Json;
using System.Text;

namespace MosqueMateV2.Domain.Repositories
{
    public class JsonQuestionRepository : IJsonQuestionRepository
    {
        private readonly List<DTOQuestions> _Obj = [];
        private bool disposedValue;
        IResourceManagerRepository _resourceManager;

        public JsonQuestionRepository(HistoricTypesEnum types, int levelNO)
        {
            if (levelNO > 3)
                return;

            _resourceManager = new ResourceManagerRepository(ResourceTypeEnum.FileResources);
            var fileName = $"{types}-level-{levelNO}";
            var fileByte = _resourceManager.GetResourceByte(fileName);
            string jsonContent = Encoding.UTF8.GetString(fileByte);
            _Obj = JsonConvert.DeserializeObject<List<DTOQuestions>>(jsonContent);
        }

        public List<DTOQuestions> GetAllQuestions()
        {
            return _Obj;
        }
        public DTOQuestions GetQuestionById(int id)
        {
            var res = _Obj.FirstOrDefault(x => x.id == id) ?? new();
            return res;
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
            _Obj.Clear();
            _resourceManager.Dispose();
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public int GetAllQuestionsCount()
        {
            return _Obj.Count;
        }

        public DTOQuestions GetRandomQuestion()
        {
            var random = new Random().Next(1, _Obj.Count);
            var res = _Obj.ElementAt(random);
            return res;
        }
    }
}

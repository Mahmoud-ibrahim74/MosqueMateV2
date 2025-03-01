using MosqueMateV2.Domain.DTOs;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface ILinkRepository
    {
        public Task<List<DTOQuranLinks>> GetAllLinks();
        public DTOQuranLinks GetLinkByName(string name);
        public string ModifyOneLink(List<string> urls);

    }
}

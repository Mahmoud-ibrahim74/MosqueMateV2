using MosqueMateV2.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface ILinkRepository
    {
        public Task<List<DTOQuranLinks>> GetAllLinks();
        public DTOQuranLinks GetLinkByName(string name);
    }
}

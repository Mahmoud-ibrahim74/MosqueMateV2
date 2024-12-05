using MosqueMateV2.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MosqueMateV2.Domain.Interfaces
{
    public interface ISuraRepository
    {
        public Task<List<DTOSuraNames>> GetAllSuraNames();
        public DTOSuraNames GetSuranByName(string name);
        public DTOSuraNames GetSuraById(int id);
    }
}

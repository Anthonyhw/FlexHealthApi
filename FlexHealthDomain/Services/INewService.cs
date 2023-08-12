using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Services
{
    public interface INewService
    {
        Task<IEnumerable<Noticia>> GetNews();
        Task<Noticia> GetNewById(int id);
        bool CreateNew(NoticiaDto createNew);
    }
}

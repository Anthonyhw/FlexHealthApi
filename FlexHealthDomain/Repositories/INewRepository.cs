using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Repositories
{
    public interface INewRepository: IGeneralRepository
    {
        Task<IEnumerable<Noticia>> GetNews();
        Task<Noticia> GetNewById(int id);
        bool CreateNew(Noticia createNew);
    }
}

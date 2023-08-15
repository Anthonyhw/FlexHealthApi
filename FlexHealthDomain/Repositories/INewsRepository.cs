using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Repositories
{
    public interface INewsRepository: IGeneralRepository
    {
        Task<IEnumerable<Noticia>> GetNews();
        Task<Noticia> GetNewsById(int id);
        bool CreateNews(Noticia createNew);

        bool RemoveNews(int id);
    }
}

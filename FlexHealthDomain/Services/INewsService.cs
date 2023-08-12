using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Services
{
    public interface INewsService
    {
        Task<IEnumerable<Noticia>> GetNews();
        Task<Noticia> GetNewsById(int id);
        bool CreateNews(NoticiaDto createNew);
    }
}

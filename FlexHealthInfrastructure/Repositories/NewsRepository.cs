using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthInfrastructure.Repositories
{
    public class NewsRepository : GeneralRepository, INewsRepository
    {
        private readonly FlexHealthContext _context;
        public NewsRepository(FlexHealthContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Noticia>> GetNews()
        {
            return await _context.tfh_noticias.Where(n => n.DataCriacao > DateTime.Now.AddDays(-15) && n.DataCriacao < DateTime.Now.AddDays(15)).ToListAsync();
        }

        public async Task<Noticia> GetNewsById(int id)
        {
            return await _context.tfh_noticias.FirstOrDefaultAsync(n => n.Id == id);
        }

        public bool CreateNews(Noticia createNew)
        {
            var newCreation = _context.tfh_noticias.Add(createNew);
            return _context.SaveChanges() > 0;
        }

        public bool RemoveNews(int id) {
            var news = _context.tfh_noticias.Where(n => n.Id == id).FirstOrDefault();
            _context.tfh_noticias.Remove(news);
            return _context.SaveChanges() > 0;
        }
    }
}

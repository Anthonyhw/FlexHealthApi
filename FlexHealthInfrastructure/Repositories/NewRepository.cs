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
    public class NewRepository : GeneralRepository, INewRepository
    {
        private readonly FlexHealthContext _context;
        public NewRepository(FlexHealthContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Noticia>> GetNews()
        {
            return await _context.tfh_noticias.Where(n => n.DataCriacao.Month == DateTime.Now.Month && n.DataCriacao.Year == DateTime.Now.Year).ToListAsync();
        }

        public async Task<Noticia> GetNewById(int id)
        {
            return await _context.tfh_noticias.FirstOrDefaultAsync(n => n.Id == id);
        }

        public bool CreateNew(Noticia createNew)
        {
            var newCreation = _context.tfh_noticias.Add(createNew);
            return _context.SaveChanges() > 0;
        }
    }
}

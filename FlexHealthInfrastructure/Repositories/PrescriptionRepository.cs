using FlexHealthDomain.DTOs;
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
    public class PrescriptionRepository: GeneralRepository, IPrescriptionRepository
    {
        private readonly FlexHealthContext _context;
        public PrescriptionRepository(FlexHealthContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Prescricao> GetPrescription(int id)
        {
            return await _context.tfh_prescricoes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Prescricao>> GetPrescriptionsByUserId(int id)
        {
            return await _context.tfh_prescricoes.Where(p => p.UsuarioId == id).ToListAsync();
        }

        public async Task<IEnumerable<Prescricao>> GetPrescriptionsByScheduleId(int id)
        {
            return await _context.tfh_prescricoes.Where(p => p.AgendamentoId == id).ToListAsync();
        }
    }
}

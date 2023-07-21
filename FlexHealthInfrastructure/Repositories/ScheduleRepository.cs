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
    public class ScheduleRepository: GeneralRepository, IScheduleRepository
    {
        private readonly FlexHealthContext _context;
        public ScheduleRepository(FlexHealthContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Agendamento> GetScheduleAsync(HorarioDto horario)
        {
            var response = _context.tfh_agendamentos.FirstOrDefault(a => a.DataConsulta.Date.Equals(horario.Hora.Date) && a.DataConsulta.Hour.Equals(horario.Hora.AddHours(-3).Hour) && a.DataConsulta.Minute.Equals(horario.Hora.AddHours(-3).Minute));
            return response;
        }

        public async Task<Agendamento> GetScheduleByIdAsync(int id)
        {
            var response = await _context.tfh_agendamentos.FirstOrDefaultAsync(s => s.Id == id);
            return response;
        }

        public async Task<List<Agendamento>> GetScheduleByPatientIdAsync(int id)
        {
            var response = await _context.tfh_agendamentos.Where(s => s.UsuarioId == id).Include(m => m.Medico).Include(m => m.Usuario).ToListAsync();
            return response;
        }
        public async Task<List<Agendamento>> GetScheduleByStablishmentIdAsync(int id)
        {
            var response = await _context.tfh_agendamentos.Where(s => s.EstabelecimentoId == id).Include(m => m.Medico).Include(m => m.Usuario).ToListAsync();
            return response;
        }
        public async Task<List<Agendamento>> GetScheduleByDoctorIdAsync(int id)
        {
            var response = await _context.tfh_agendamentos.Where(s => s.MedicoId == id).Include(u => u.Usuario).ToListAsync();
            return response;
        }

        public async Task<List<Agendamento>> GetScheduleByCityAsync(string city)
        {
            var response = await _context.tfh_agendamentos.Where(s => (s.Estabelecimento.Endereco.ToUpper().Contains($@"\{city.ToUpper()}\") && (s.Status == "Aberto") )).Include(m => m.Medico).ToListAsync();
            return response;
        }

        public async Task<Agendamento> ScheduleToUser(AgendamentoParaUsuarioDto agendamento)
        {
            var response = await _context.tfh_agendamentos.Where(s => s.Id == agendamento.AgendamentoId).FirstOrDefaultAsync();
            response.UsuarioId = agendamento.UsuarioId;
            response.DataMarcacao = DateTime.Now;
            response.Pagamento = agendamento.Pagamento;
            response.Status = "Agendado";
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<Agendamento> CancelSchedule(int id)
        {
            var response = await _context.tfh_agendamentos.Where(sch => sch.Id == id).FirstOrDefaultAsync();
            response.Status = "Cancelado";
            _context.SaveChanges();
            return response;
        }

        public async Task<bool> DeleteSchedule(int id)
        {
            _context.tfh_agendamentos.Where(sch => sch.Id == id).ExecuteDelete();
            var response = await _context.SaveChangesAsync();
            return response > -1;
        }
    }
}

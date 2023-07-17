using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Repositories
{
    public interface IScheduleRepository: IGeneralRepository
    {
        Task<Agendamento> GetScheduleAsync(HorarioDto horario);
        Task<Agendamento> GetScheduleByIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByPatientIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByStablishmentIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByDoctorIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByCityAsync(string city);
        Task<Agendamento> ScheduleToUser(AgendamentoParaUsuarioDto agendamento);
    }
}

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
        Task<Agendamento> GetScheduleAsync(HorarioDto horario, int medicoId);
        Task<Agendamento> GetScheduleByIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByPatientIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByStablishmentIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByStablishmentAsync(string stablishment);
        Task<List<Agendamento>> GetScheduleByDoctorIdAsync(int id);
        Task<List<Agendamento>> GetScheduleByCityAsync(string city);
        Task<Agendamento> ScheduleToUser(AgendamentoParaUsuarioDto agendamento);
        Task<Agendamento> CancelSchedule(int id);
        bool ApprovePayment(int id);
        Task<bool> DeleteSchedule(int id);
        Agendamento EndSchedule(int id);
    }
}

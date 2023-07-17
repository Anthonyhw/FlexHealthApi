using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Services
{
    public interface IScheduleService
    {
        Task<AgendamentoDto> CreateSchedule(AgendamentoDto datas);
        Task<AgendaDto> GetScheduleByIdAsync(int id);
        Task<List<AgendaDto>> GetScheduleByPatientIdAsync(int id);
        Task<List<AgendaDto>> GetScheduleByStablishmentIdAsync(int id);
        Task<List<AgendaDto>> GetScheduleByDoctorIdAsync(int id);
        Task<List<AgendaPorEstabelecimentoDto>> GetScheduleByCityAsync(string city);
        Task<AgendamentoDto> ScheduleToUser(AgendamentoParaUsuarioDto agendamento);
    }
}

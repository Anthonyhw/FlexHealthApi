using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthDomain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthInfrastructure.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        public ScheduleService(IScheduleRepository scheduleRepository) { 
            _scheduleRepository = scheduleRepository;
        }
        public async Task<Agendamento> CreateSchedule(AgendamentoDto datas)
        {
            try
            {
                foreach (var data in datas.Datas)
                {
                    foreach (var horario in data.Horarios)
                    {
                        if (_scheduleRepository.GetSchedule(horario) == null)
                        {
                            _scheduleRepository.Add(new Agendamento()
                            {
                                DataConsulta = horario.Hora.AddHours(-3),
                                MedicoId = datas.MedicoId,
                                EstabelecimentoId = datas.EstabelecimentoId,
                                Status = "Aberto",
                                Tipo = datas.Tipo,
                                Especialidade = datas.Especialidade,
                                Valor = "R$ " + float.Parse(horario.Valor).ToString(),
                            });
                        }
                    }
                }

                if (await _scheduleRepository.SaveChangesAsync())
                {
                    //?
                }
                return null;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

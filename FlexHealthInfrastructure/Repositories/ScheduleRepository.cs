using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthInfrastructure.Context;
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

        public Agendamento GetSchedule(HorarioDto horario)
        {
            var response = _context.tfh_agendamentos.FirstOrDefault(a => a.DataConsulta.Date.Equals(horario.Hora.Date) && a.DataConsulta.Hour.Equals(horario.Hora.AddHours(-3).Hour) && a.DataConsulta.Minute.Equals(horario.Hora.AddHours(-3).Minute));
            return response;
        }
    }
}

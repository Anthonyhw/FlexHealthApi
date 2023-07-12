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
        Agendamento GetSchedule(HorarioDto horario);
    }
}

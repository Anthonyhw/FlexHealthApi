using FlexHealthDomain.DTOs;
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
    }
}

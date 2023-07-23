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

    }
}

using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Repositories
{
    public interface IPrescriptionRepository: IGeneralRepository
    {
        Task<Prescricao> GetPrescription(int id);
        Task<IEnumerable<Prescricao>> GetPrescriptionsByUserId(int id);
        Task<IEnumerable<Prescricao>> GetPrescriptionsByScheduleId(int id);
    }
}

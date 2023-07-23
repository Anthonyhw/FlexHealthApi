using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Services
{
    public interface IPrescriptionService
    {
        Task<ArquivoDto[]> CreatePrescription(ArquivoDto[] arquivos);
    }
}

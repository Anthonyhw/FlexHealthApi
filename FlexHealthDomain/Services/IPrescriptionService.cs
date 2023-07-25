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
        Task<ArquivoDto> GetPrescription(int id);
        Task<IEnumerable<ArquivoDto>> GetPrescriptionsByUserId(int id, bool visibleOnly);
        Task<IEnumerable<ArquivoDto>> GetPrescriptionsByScheduleId(int id);
        Task<IEnumerable<ArquivoDto>> CreatePrescription(ArquivoDto[] arquivos);
        Byte[] DownloadPrescription(string fileName);
        bool ChangePrescriptionVisibility(int id, bool visibility);
    }
}

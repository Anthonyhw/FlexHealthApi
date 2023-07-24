using AutoMapper;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FlexHealthInfrastructure.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _PrescriptionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;
        public PrescriptionService(IPrescriptionRepository PrescriptionRepository, IMapper mapper, IAccountRepository accountRepository, IHostingEnvironment environment)
        {
            _PrescriptionRepository = PrescriptionRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<ArquivoDto> GetPrescription(int id)
        {
            try
            {
                var prescription = await _PrescriptionRepository.GetPrescription(id);
                if (prescription != null)
                {
                    var result = _mapper.Map<ArquivoDto>(prescription);
                    return result;
                }
                return null;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<ArquivoDto>> GetPrescriptionsByUserId(int id, bool visibleOnly = false)
        {
            try
            {
                var prescription = await _PrescriptionRepository.GetPrescriptionsByUserId(id, visibleOnly);
                if (prescription != null)
                {
                    var result = _mapper.Map<IEnumerable<ArquivoDto>>(prescription);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<ArquivoDto>> GetPrescriptionsByScheduleId(int id)
        {
            try
            {
                var prescription = await _PrescriptionRepository.GetPrescriptionsByScheduleId(id);
                if (prescription != null)
                {
                    var result = _mapper.Map<IEnumerable<ArquivoDto>>(prescription);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ArquivoDto>> CreatePrescription(ArquivoDto[] arquivos)
        {
            try
            {
                foreach (var arquivo in arquivos)
                {
                    _PrescriptionRepository.Add(new Prescricao()
                    {
                        URL = arquivo.URL,
                        UsuarioId = arquivo.UsuarioId,
                        AgendamentoId = arquivo.AgendamentoId,
                        MedicoId = arquivo.MedicoId,
                        Proposito = arquivo.Proposito,
                        Visibilidade = false
                    });
                    string uploadFolder = Path.Combine(_environment.ContentRootPath + @"Resources\Prescriptions\" + arquivo.URL + Path.GetExtension(arquivo.Arquivo.FileName));
                    using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                    {
                        arquivo.Arquivo.CopyTo(fileStream);
                    }
                }

                if (await _PrescriptionRepository.SaveChangesAsync())
                {
                    return arquivos;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

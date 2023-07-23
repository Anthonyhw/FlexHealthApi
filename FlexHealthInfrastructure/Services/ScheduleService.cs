using AutoMapper;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthDomain.Services;
using System;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace FlexHealthInfrastructure.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;
        public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, IAccountRepository accountRepository, IPrescriptionRepository prescriptionRepository, IHostingEnvironment environment)
        {
            _scheduleRepository = scheduleRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _prescriptionRepository = prescriptionRepository;
            _environment = environment;
        }
        public async Task<AgendamentoDto> CreateSchedule(AgendamentoDto datas)
        {
            try
            {
                foreach (var data in datas.Datas)
                {
                    foreach (var horario in data.Horarios)
                    {
                        if (await _scheduleRepository.GetScheduleAsync(horario) == null)
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
        public bool EndSchedule(EncerrarAgendamentoDto request)
        {
            bool verifyAdds = true; 
            try
            {
                var schedule = _scheduleRepository.EndSchedule(int.Parse(request.AgendamentoId));
                if (schedule != null)
                {
                    try
                    {
                        if (request.Arquivos != null)
                        {
                            foreach (var arquivo in request.Arquivos)
                            {
                                _prescriptionRepository.Add(new Prescricao()
                                {
                                    URL = arquivo.URL,
                                    UsuarioId = arquivo.UsuarioId,
                                    MedicoId = arquivo.MedicoId,
                                    Proposito = arquivo.Proposito,
                                    AgendamentoId = arquivo.AgendamentoId,
                                    Visibilidade = false
                                });
                                string uploadFolder = Path.Combine(_environment.ContentRootPath + @"Resources\Prescriptions\" + arquivo.URL + Path.GetExtension(arquivo.Arquivo.FileName));
                                using (var fileStream = new FileStream(uploadFolder, FileMode.Create)) { 
                                    arquivo.Arquivo.CopyTo(fileStream);
                                }
                                
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        verifyAdds = false;
                        throw new Exception(ex.Message);
                    }
                    if (verifyAdds)
                    {
                        return _scheduleRepository.SaveChanges();
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgendaDto> GetScheduleByIdAsync(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
                if (schedule != null)
                {
                    var result = _mapper.Map<AgendaDto>(schedule);
                    return result;
                }
                return null;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AgendaDto>> GetScheduleByPatientIdAsync(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.GetScheduleByPatientIdAsync(id);
                if (schedule != null)
                {
                    var result = _mapper.Map<List<AgendaDto>>(schedule);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AgendaDto>> GetScheduleByDoctorIdAsync(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.GetScheduleByDoctorIdAsync(id);
                if (schedule != null)
                {
                    var result = _mapper.Map<List<AgendaDto>>(schedule);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AgendaDto>> GetScheduleByStablishmentIdAsync(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.GetScheduleByStablishmentIdAsync(id);
                if (schedule != null)
                {
                    var result = _mapper.Map<List<AgendaDto>>(schedule);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<AgendaPorEstabelecimentoDto>> GetScheduleByCityAsync(string city)
        {
            try
            {
                var schedule = await _scheduleRepository.GetScheduleByCityAsync(city);
                if (schedule != null)
                {
                    var mappedSchedule = _mapper.Map<List<AgendaDto>>(schedule);
                    var result = new List<AgendaPorEstabelecimentoDto>();

                    foreach ( var item in mappedSchedule)
                    {
                        if (result.Find(s => s.Estabelecimento.Id == item.EstabelecimentoId) == null)
                        {
                            var estabelecimento = await _accountRepository.GetUserByIdAsync(item.EstabelecimentoId);
                            result.Add(new AgendaPorEstabelecimentoDto() { Estabelecimento = estabelecimento, Agenda = new List<AgendaDto>()});
                        }
                        result.Find(s => s.Estabelecimento.Id == item.EstabelecimentoId).Agenda.Add(item);
                    }

                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgendamentoDto> ScheduleToUser(AgendamentoParaUsuarioDto agendamento)
        {
            try
            {
                var scheduleToUser = await _scheduleRepository.ScheduleToUser(agendamento);
                if (scheduleToUser != null)
                {
                    await _scheduleRepository.SaveChangesAsync();
                    var map = _mapper.Map<AgendamentoDto>(scheduleToUser);
                    return map;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AgendaDto> CancelSchedule(int id)
        {
            try
            {
                await _scheduleRepository.SaveChangesAsync();
                var updateResult = await _scheduleRepository.CancelSchedule(id);
                if (updateResult != null)
                {
                    var response = _mapper.Map<AgendaDto>(updateResult);
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> DeleteSchedule(int id)
        {
            try
            {
                var deleteResult = _scheduleRepository.DeleteSchedule(id);
                if (deleteResult.Result) _scheduleRepository.SaveChangesAsync();
                return deleteResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

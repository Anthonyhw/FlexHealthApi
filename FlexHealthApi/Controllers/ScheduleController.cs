using FlexHealthApi.Extensions;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FlexHealthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSchedule(AgendamentoDto datas)
        {
            try
            {
                var result = await _scheduleService.CreateSchedule(datas);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar agenda: {ex.Message}");
            }
        }

        [HttpPost("end")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public IActionResult EndSchedule([FromForm] EncerrarAgendamentoDto request)
        {
            try
            {
                var result = _scheduleService.EndSchedule(request);
                if (result) return Ok(result);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar fechar agenda: {result}");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar fechar agenda: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetScheduleByScheduleId(int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByIdAsync(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar recuperar agenda: {ex.Message}");
            }
        }

        [HttpGet("Patient")]
        [Authorize]
        public async Task<IActionResult> GetScheduleByPatientId([FromQuery] int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByPatientIdAsync(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar recuperar agenda: {ex.Message}");
            }
        }

        [HttpGet("Doctor")]
        [Authorize(Roles = "Medico,Estabelecimento")]
        public async Task<IActionResult> GetScheduleByDoctorId([FromQuery] int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByDoctorIdAsync(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar recuperar agenda: {ex.Message}");
            }
        }

        [HttpGet("Stablishment")]
        [Authorize(Roles = "Estabelecimento")]
        public async Task<IActionResult> GetScheduleByStablishmentId([FromQuery] int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByStablishmentIdAsync(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar recuperar agenda: {ex.Message}");
            }
        }

        [HttpGet("City")]
        [Authorize]
        public async Task<IActionResult> GetScheduleByCityId([FromQuery] string city)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByCityAsync(city);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar recuperar agenda: {ex.Message}");
            }
        }

        [HttpPut("user")]
        [Authorize]
        public async Task<IActionResult> ScheduleToUser([FromBody] AgendamentoParaUsuarioDto agendamento)
        {
            try
            {
                agendamento.UsuarioId = User.Id();
                var result = await _scheduleService.ScheduleToUser(agendamento);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar Confirmar Agendamento: {ex.Message}");

            }
        }

        [HttpPut("cancel")]
        [Authorize(Roles = "Medico,Paciente")]
        public async Task<IActionResult> CancelSchedule([FromBody] int id)
        {
            try
            {
                var result = await _scheduleService.CancelSchedule(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar Confirmar Agendamento: {ex.Message}");

            }
        }

        [HttpDelete]
        [Authorize(Roles ="Medico")]
        public async Task<IActionResult> DeleteSchedule([FromQuery] int id)
        {
            try
            {
                var result = await _scheduleService.DeleteSchedule(id);
                if (result != null) return Ok(result);
                return StatusCode(500, "Não foi possível deletar o Agendamento!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar Deletar Agendamento: {ex.Message}");

            }
        }
    }
}

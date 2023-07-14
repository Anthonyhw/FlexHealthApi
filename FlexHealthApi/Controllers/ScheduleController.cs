using FlexHealthDomain.DTOs;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexHealthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController: ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSchedule(AgendamentoDto datas)
        {
            try {
                 var result = await _scheduleService.CreateSchedule(datas);
                 return Ok(result);

            }catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar agenda: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByIdAsync(id); 
                if (result != null) return Ok(result);
                return NotFound();
            }catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar criar agenda: {ex.Message}");
            }
        }

        [HttpGet("Patient/{id}")]
        public async Task<IActionResult> GetScheduleByPatientId(int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByPatientIdAsync(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar criar agenda: {ex.Message}");
            }
        }

        [HttpGet("Doctor/{id}")]
        [Authorize(Roles="Medico,Estabelecimento")]
        public async Task<IActionResult> GetScheduleByDoctorId(int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByDoctorIdAsync(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar criar agenda: {ex.Message}");
            }
        }

        [HttpGet("Stablishment/{id}")]
        [Authorize(Roles = "Estabelecimento")]
        public async Task<IActionResult> GetScheduleByStablishmentId(int id)
        {
            try
            {
                var result = await _scheduleService.GetScheduleByStablishmentIdAsync(id);
                if (result != null) return Ok(result);
                return NotFound();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar criar agenda: {ex.Message}");
            }
        }
    }
}

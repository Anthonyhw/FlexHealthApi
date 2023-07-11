using FlexHealthDomain.DTOs;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexHealthApi.Controllers
{
    [Authorize(Roles = "Medico,Estabelecimento")]
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
        public async Task<ActionResult<AgendamentoDto>> CreateSchedule(AgendamentoDto datas)
        {
            try {
                 var result = await _scheduleService.CreateSchedule(datas);
                 return Ok(result);

            }catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar criar agenda: {ex.Message}");
            }
        }
    }
}

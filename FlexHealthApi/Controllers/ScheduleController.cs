using FlexHealthApi.Extensions;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using System.Drawing;

namespace FlexHealthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IWebHostEnvironment _environment;

        public ScheduleController(IScheduleService scheduleService, IWebHostEnvironment environment)
        {
            _scheduleService = scheduleService;
            _environment = environment;
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

        [HttpGet("approve")]
        [AllowAnonymous]
        public IActionResult ApprovePayment([FromQuery] int id)
        {
            try
            {
                var result = _scheduleService.ApprovePayment(id);
                if (result != null) return Ok("Agendamento Pago com sucesso!");
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

        [HttpGet("QrCode")]
        [AllowAnonymous]
        public IActionResult GetQrCode([FromQuery] string url, [FromQuery] int id)
        {
            try
            {
                // Criar o gerador do QR Code
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

                // Gerar a imagem do QR Code
                var qrCodeImage = qrCode.GetGraphic(20);

                Bitmap qrCodeFile;
                using (var ms = new MemoryStream(qrCodeImage))
                {
                    qrCodeFile = new Bitmap(ms);
                }

                // Salvar a imagem em uma pasta
                var imagePath = Path.Combine((_environment.ContentRootPath + @"Resources\QrCode\"));
                if (!Directory.Exists(imagePath))
                {
                    Directory.CreateDirectory(imagePath);
                }

                string imageName = $"schedule{id}QRCode.png";
                string fullPath = Path.Combine(imagePath, imageName);
                qrCodeFile.Save(fullPath, ImageFormat.Png);

                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"Erro ao tentar Agendar horário: {ex.Message}");

            }
        }
    }
}

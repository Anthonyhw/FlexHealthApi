using FlexHealthApi.Extensions;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Models;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Net.Http.Headers;

namespace FlexHealthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _PrescriptionService;

        public PrescriptionController(IPrescriptionService PrescriptionService)
        {
            _PrescriptionService = PrescriptionService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetPrescrpition(int id)
        {
            try
            {
                var result = await _PrescriptionService.GetPrescription(id);
                if (result != null) return Ok(result);
                return NoContent();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar prescrições: {ex.Message}");
            }
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetPrescrpitionsByUserId(int id, bool visibleOnly = false)
        {
            try
            {
                var result = await _PrescriptionService.GetPrescriptionsByUserId(id, visibleOnly);
                if (result != null) return Ok(result);
                return NoContent();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar prescrições: {ex.Message}");
            }
        }

        [HttpGet("schedule/{id}")]
        [Authorize]
        public async Task<IActionResult> GetPrescrpitionsByScheduleId(int id)
        {
            try
            {
                var result = await _PrescriptionService.GetPrescriptionsByScheduleId(id);
                if (result != null) return Ok(result);
                return NoContent();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar prescrições: {ex.Message}");
            }
        }

        [HttpPost("download")]
        public IActionResult DownloadPrescription([FromBody] string fileName)
        {
            try
            {
                var result = _PrescriptionService.DownloadPrescription(fileName);
                Response.Headers.Add("Content-Disposition", new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                }.ToString());
                if (result != null) return File(result, "application/pdf", fileName.Substring(0, fileName.IndexOf("_user")) + ".pdf");
                return NoContent();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar prescriçãp: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Medico")]
        public async Task<ActionResult> CreatePrescription([FromForm] ArquivoDto[] arquivos)
        {
            try
            {
                var result = await _PrescriptionService.CreatePrescription(arquivos);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar enviar prescrições: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult ChangePrescriptionVisibility(int id, [FromBody] bool visibility)
        {
            try
            {
                var result = _PrescriptionService.ChangePrescriptionVisibility(id, visibility);
                if (result) return Ok(result);
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar alterar visibilidade");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar enviar prescrições: {ex.Message}");
            }
        }
    }
}

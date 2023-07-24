﻿using FlexHealthApi.Extensions;
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
        public async Task<IActionResult> GetPrescrpitionsByUserId(int id)
        {
            try
            {
                var result = await _PrescriptionService.GetPrescriptionsByUserId(id);
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

        [HttpPost]
        [Authorize(Roles="Medico")]
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
    }
}

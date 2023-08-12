using FlexHealthDomain.DTOs;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexHealthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NewController : ControllerBase
    {
        private readonly INewService _newService;

        public NewController(INewService newService)
        {
            _newService = newService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                var result = await _newService.GetNews();
                if (result != null) return Ok(result);
                return NoContent();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Notícias: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetNewById(int id)
        {
            try
            {
                var result = await _newService.GetNewById(id);
                if (result != null) return Ok(result);
                return NoContent();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Notícia: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Estabelecimento")]
        public ActionResult CreateNew([FromForm] NoticiaDto noticia)
        {
            try
            {
                var result = _newService.CreateNew(noticia);
                if (result) return Ok(result);
                else return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Criar Notícia!");

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar enviar prescrições: {ex.Message}");
            }
        }
    }
}

using FlexHealthDomain.DTOs;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlexHealthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetNews()
        {
            try
            {
                var result = await _newsService.GetNews();
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
        public async Task<IActionResult> GetNewsById(int id)
        {
            try
            {
                var result = await _newsService.GetNewsById(id);
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
        public ActionResult CreateNews([FromForm] NoticiaDto noticia)
        {
            try
            {
                var result = _newsService.CreateNews(noticia);
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

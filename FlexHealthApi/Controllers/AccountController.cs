using FlexHealthApi.Extensions;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Identity;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlexHealthApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userEmail = User.Email();
                var user = _accountService.GetUser(userEmail);
                return Ok(user.Result);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuário: {ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = _accountService.GetUserById(id);
                if (user == null) return NoContent();
                return Ok(user.Result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuário: {ex.Message}");
            }
        }

        [HttpGet("GetDoctors")]
        [Authorize(Roles="Estabelecimento")]
        public async Task<IActionResult> GetDoctors()
        {
            try
            {
                var user = _accountService.GetDoctors(User.Id());
                if (user == null) return NoContent();
                return Ok(user.Result);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar Médicos: {ex.Message}");
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            try
            {
                if (await _accountService.VerifyCpf(userDto.Cpf)) return BadRequest("CPF já cadastrado no sistema!");
                if (await _accountService.VerifyRg(userDto.Rg)) return BadRequest("RG já cadastrado no sistema!");
                if (await _accountService.VerifyEmail(userDto.Email)) return BadRequest("E-mail já cadastrado no sistema!");
                if (userDto.Crm !=  null)
                {
                    if (await _accountService.VerifyCrm(userDto.Crm)) return BadRequest("CRM já cadastrado no sistema!");
                }


                var user = await _accountService.CreateAccount(userDto);
                if (user != null) return Ok(user);
                return BadRequest("Não foi possível criar o usuário!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuário: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUser(userLogin.Username);
                if (user == null) user = await _accountService.GetUser(userLogin.Email);
                if (user == null) return Unauthorized("Usuário ou senha inválidos!");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized("Usuário ou senha inválidos!");

                return Ok(new
                {
                    Username = user.UserName,
                    Nome = user.Nome,
                    Token = _tokenService.GetToken(user).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar usuário: {ex.Message}");
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto user)
        {
            try
            {
                var checkUser = await _accountService.GetUser(user.UserName);
                if (checkUser == null) return Unauthorized("Usuário inválido!");
                
                user.Id = checkUser.Id;

                var result = await _accountService.UpdateAccount(user);
                if (result == null) return NoContent();

                return Ok(new
                {
                    Username = result.UserName,
                    Nome = result.Nome,
                    Token = _tokenService.GetToken(checkUser).Result
                });
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar usuário: {ex.Message}");
            }
        }

        [HttpPut("Image")]
        [Authorize]
        public async Task<IActionResult> UpdatePhotoAsync([FromForm] IFormFile file, string FileName)
        {
            try
            {
                var updatePhoto = await _accountService.UpdatePhotoAsync(file, User.Id(), FileName);
                if (!updatePhoto) return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar imagem, tente novamente mais tarde.");
                return Ok(updatePhoto);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar Atualizar imagem: {ex.Message}");
            }
        }

        [HttpPost("Claim")]
        public async Task<IActionResult> AddClaim([FromBody] string claim, string value)
        {
            var result = _accountService.AddClaim(User.Email(), claim, value);
            if (result == null) return NoContent();
            return Ok(result);
        }

        [HttpPost("Role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            var result = _accountService.AddRole(User.Email(), role); 
            if (result == null) return NoContent();
            return Ok() ;
        }

        [HttpPost("Role/Create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRole([FromBody] string role)
        {
            var result = _accountService.CreateRole(role);
            if (result == null) return NoContent();
            return Ok();
        }
    }
}

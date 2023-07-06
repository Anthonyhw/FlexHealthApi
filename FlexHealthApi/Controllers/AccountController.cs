﻿using FlexHealthApi.Extensions;
using FlexHealthDomain.DTOs;
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

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            try
            {
                if (await _accountService.VerifyCpf(userDto.Cpf)) return BadRequest("CPF já cadastrado no sistema!");
                if (await _accountService.VerifyRg(userDto.Rg)) return BadRequest("RG já cadastrado no sistema!");
                if (await _accountService.VerifyEmail(userDto.Email)) return BadRequest("E-mail já cadastrado no sistema!");
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
                if (user == null) return Unauthorized("Usuário inválido!");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (!result.Succeeded) return Unauthorized();

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
    }
}

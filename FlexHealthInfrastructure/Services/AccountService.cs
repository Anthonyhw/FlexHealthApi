using AutoMapper;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace FlexHealthInfrastructure.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _environment;

        public AccountService(IAccountRepository accountRepository, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, RoleManager<Role> roleManager, IHostingEnvironment environment)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _environment = environment;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserDto userUpdateDto, string password)
        {
            try
            {
                var user = _userManager.Users.SingleOrDefault(user => user.UserName == userUpdateDto.UserName.ToLower());

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar password: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccount(RegisterUserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    if (userDto.Tipo == "Clinica" || userDto.Tipo == "Consultorio")
                    {
                        await _userManager.AddToRoleAsync(user, "Estabelecimento");
                        await _userManager.AddClaimAsync(user, new Claim("CNPJ", userDto.Cnpj));
                        await _userManager.AddClaimAsync(user, new Claim("Tipo", userDto.Tipo));
                    }
                    else if (userDto.Tipo == "Medico")
                    {
                        await _userManager.AddToRoleAsync(user, "Medico");
                        await _userManager.AddClaimAsync(user, new Claim("Estabelecimento", userDto.EstabelecimentoId));
                        await _userManager.AddClaimAsync(user, new Claim("CRM", userDto.Crm));
                        await _userManager.AddClaimAsync(user, new Claim("Especialidade", userDto.Especialidade));
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "Paciente");
                    }

                    var userToReturn = _mapper.Map<UserDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar criar usuário: {ex.Message}");
            }
        }

        public async Task<UserDto> GetUser(string email)
        {
            try
            {
                var user = await _accountRepository.GetUserAsync(email);
                if (user == null) return null;

                var userDto = _mapper.Map<UserDto>(user);

                var claims = await _userManager.GetClaimsAsync(user);
                userDto.Claims = claims.Select(c => new ClaimsDto() { Type = c.Type, Value = c.Value });

                userDto.Roles = await _userManager.GetRolesAsync(user);
                
                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar usuário: {ex.Message}");
            }
        }

        public async Task<UserDto> GetUserById(int id)
        {
            try
            {
                var user = await _accountRepository.GetUserByIdAsync(id);
                if (user == null) return null;

                var userDto = _mapper.Map<UserDto>(user);

                var claims = await _userManager.GetClaimsAsync(user);
                userDto.Claims = claims.Select(c => new ClaimsDto() { Type = c.Type, Value = c.Value });

                userDto.Roles = await _userManager.GetRolesAsync(user);

                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar usuário: {ex.Message}");
            }
        }

        public async Task<List<UserDto>> GetDoctors(int id)
        {
            try
            {
                var users = await _accountRepository.GetDoctors(id);
                if (users == null) return null;

                var usersDto = _mapper.Map<List<UserDto>>(users);

                foreach(var user in usersDto)
                {
                    var claims = await _userManager.GetClaimsAsync(users.First(u => u.Id == user.Id));
                    foreach (var u in usersDto)
                    {
                        u.Claims = claims.Select(c => new ClaimsDto() { Type = c.Type, Value = c.Value });

                        u.Roles = await _userManager.GetRolesAsync(users.First(u => u.Id == user.Id));
                    }

                }

                return usersDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar usuário: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _accountRepository.GetUserAsync(userUpdateDto.Email);
                if (user == null) return null;

                var updatedUser = _mapper.Map(userUpdateDto, user);

                if (userUpdateDto.Password != "") {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    await _userManager.ResetPasswordAsync(user, token, userUpdateDto.Password);
                }

                if (await _accountRepository.SaveChangesAsync())
                {
                    var userRetorno = await _accountRepository.GetUserAsync(user.Email);

                    return _mapper.Map<UserUpdateDto>(userRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário: {ex.Message}");
            }
        }

        public async Task<bool> UpdatePhotoAsync(IFormFile file, int id, string FileName)
        {
            try
            {
                FileName = $"{FileName}{Path.GetExtension(file.FileName)}";
                var response = _accountRepository.UpdatePhotoAsync(id, FileName);
                if (response.Result)
                {
                    string uploadFolder = Path.Combine(_environment.ContentRootPath + @"Resources\Images\UserImages\" + FileName);
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    using (var fileStream = new FileStream(uploadFolder, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        return (await _accountRepository.SaveChangesAsync());
                    }
                }
                return response.Result;
            }catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar imagem: {ex.Message}");
            }
        }

        public async Task<bool> VerifyCpf(string cpf)
        {
            try
            {
                return cpf == "Empresa" ? false : await _userManager.Users.AnyAsync(user => user.Cpf == cpf);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar usuário: {ex.Message}");
            }
        }
        public async Task<bool> VerifyRg(string rg)
        {
            try
            {
                return rg == "Empresa" ? false : await _userManager.Users.AnyAsync(user => user.Rg == rg);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar usuário: {ex.Message}");
            }
        }
        public async Task<bool> VerifyCrm(string crm)
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                foreach (var user in users)
                {
                    var exists = _userManager.GetClaimsAsync(user).Result.Any(u => u.Value.Equals(crm));
                    if (exists) return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar usuário: {ex.Message}");
            }
        }
        public async Task<bool> VerifyEmail(string email)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar verificar usuário: {ex.Message}");
            }
        }

        public async Task AddClaim(string email, string claim, string value)
        {
            var user = _accountRepository.GetUserAsync(email).Result;
            var result = await _userManager.AddClaimAsync(user, new Claim(claim, value));

            if (result.Succeeded)
            {
                return;
            }
            return;
        }

        public async Task AddRole(string email, string role)
        {
            var user = _accountRepository.GetUserAsync(email).Result;
            var result = await _userManager.AddToRoleAsync(user, role);

            if (result.Succeeded)
            {
                return;
            }
            return;
        }

        public async Task CreateRole(string role)
        {
            var result = _roleManager.CreateAsync(new Role() { Name = role }).Result;

            if (result.Succeeded)
            {
                return;
            }
            return;
        }
    }
}

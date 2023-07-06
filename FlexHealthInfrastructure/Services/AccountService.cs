using AutoMapper;
using FlexHealthDomain.DTOs;
using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthDomain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlexHealthInfrastructure.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
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

        public async Task<UserDto> CreateAccount(UserDto userDto)
        {
            try
            {
                var user = _mapper.Map<User>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
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

                var userUpdateDto = _mapper.Map<UserDto>(user);
                return userUpdateDto;
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

        public async Task<bool> VerifyCpf(string cpf)
        {
            try
            {
                return await _userManager.Users.AnyAsync(user => user.Cpf == cpf);
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
                return await _userManager.Users.AnyAsync(user => user.Rg == rg);
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
    }
}

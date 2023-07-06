using FlexHealthDomain.DTOs;
using FlexHealthDomain.Identity;
using Microsoft.AspNetCore.Identity;

namespace FlexHealthDomain.Services
{
    public interface IAccountService
    {
        Task<UserDto> GetUser(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserDto userUpdateDto, string password);
        Task<UserDto> CreateAccount(UserDto userDto);
        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
        Task<bool> VerifyCpf(string cpf);
        Task<bool> VerifyRg(string rg);
        Task<bool> VerifyEmail(string email);
    }
}

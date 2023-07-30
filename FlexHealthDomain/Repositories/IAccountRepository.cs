using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexHealthDomain.Repositories
{
    public interface IAccountRepository : IGeneralRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetDoctors(int id);
        Task<User> GetUserAsync(string parameter);
        Task<bool> UpdatePhotoAsync(int id, string FileName);
    }
}

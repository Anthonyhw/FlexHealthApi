using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;
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
        Task<User> GetUserAsync(string parameter);
    }
}

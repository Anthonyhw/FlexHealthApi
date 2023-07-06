using FlexHealthDomain.Identity;
using FlexHealthDomain.Models;
using FlexHealthDomain.Repositories;
using FlexHealthInfrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FlexHealthInfrastructure.Repositories
{
    public class AccountRepository: GeneralRepository, IAccountRepository
    {
        private readonly FlexHealthContext _context;
        public AccountRepository(FlexHealthContext context): base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserAsync(string parameter)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Email == parameter || user.NormalizedUserName == parameter.ToUpper());
        }
    }
}

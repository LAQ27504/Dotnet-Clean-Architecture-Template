using mudblazorclean.Application.Interfaces.Repositories;
using mudblazorclean.Domain.Entities;
using mudblazorclean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace mudblazorclean.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
            : base(context) { }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

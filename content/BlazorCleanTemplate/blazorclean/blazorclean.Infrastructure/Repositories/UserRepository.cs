using blazorclean.Application.Interfaces.Repositories;
using blazorclean.Domain.Entities;
using blazorclean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace blazorclean.Infrastructure.Repositories
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

using apiclean.Application.Interfaces.Repositories;
using apiclean.Domain.Entities;
using apiclean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace apiclean.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
            : base(context) { }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}

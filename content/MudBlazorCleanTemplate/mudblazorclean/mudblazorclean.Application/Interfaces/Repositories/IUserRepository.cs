using mudblazorclean.Domain.Entities;

namespace mudblazorclean.Application.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByEmailAsync(string email);
    }
}

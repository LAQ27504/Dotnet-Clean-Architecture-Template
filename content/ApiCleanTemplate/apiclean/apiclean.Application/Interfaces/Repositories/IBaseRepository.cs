using apiclean.Application.Common;

namespace apiclean.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task<TEntity?> GetEntityByIdAsync(Guid id);

        Task<ICollection<TEntity>> GetAllAsync();

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<(ICollection<TEntity>, int)> GetPagedAsync(
            QueryFilter<TEntity>? filter,
            int pageIndex,
            int pageSize
        );
    }
}

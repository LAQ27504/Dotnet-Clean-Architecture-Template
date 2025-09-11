using blazorclean.Application.Common;
using blazorclean.Application.Interfaces.Repositories;
using blazorclean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace blazorclean.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext _context;

        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _context = context;

            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetEntityByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<(ICollection<TEntity>, int)> GetPagedAsync(
            QueryFilter<TEntity>? filter,
            int pageIndex,
            int pageSize
        )
        {
            var query = _dbSet.AsQueryable();
            if (filter is not null)
            {
                query = filter(query);
            }

            var total = await query.CountAsync();
            var paged = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return (paged, total);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}

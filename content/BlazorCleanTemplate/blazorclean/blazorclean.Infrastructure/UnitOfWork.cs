using blazorclean.Application.Interfaces.UnitOfWork;
using blazorclean.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace blazorclean.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> CommitAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

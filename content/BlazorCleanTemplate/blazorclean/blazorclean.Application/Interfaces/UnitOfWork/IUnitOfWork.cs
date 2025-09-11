namespace blazorclean.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
        Task BeginTransactionAsync();
        Task RollbackAsync();
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}

namespace blazorclean.Application.Common
{
    public delegate IQueryable<TEntity> QueryFilter<TEntity>(IQueryable<TEntity> query);
}

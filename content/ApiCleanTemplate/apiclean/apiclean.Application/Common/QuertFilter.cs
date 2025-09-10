namespace apiclean.Application.Common
{
    public delegate IQueryable<TEntity> QueryFilter<TEntity>(IQueryable<TEntity> query);
}

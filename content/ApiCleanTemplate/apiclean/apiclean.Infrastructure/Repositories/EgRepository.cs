using apiclean.Application.Interfaces.Repositories;
using apiclean.Domain.Entities;
using apiclean.Infrastructure.Persistence;

namespace apiclean.Infrastructure.Repositories
{
    public class EgRepository : BaseRepository<EgEntity>, IEgRepository
    {
        public EgRepository(AppDbContext context)
            : base(context) { }
    }
}

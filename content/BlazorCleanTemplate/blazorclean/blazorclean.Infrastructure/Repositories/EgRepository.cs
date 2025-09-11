using blazorclean.Application.Interfaces.Repositories;
using blazorclean.Domain.Entities;
using blazorclean.Infrastructure.Persistence;

namespace blazorclean.Infrastructure.Repositories
{
    public class EgRepository : BaseRepository<EgEntity>, IEgRepository
    {
        public EgRepository(AppDbContext context)
            : base(context) { }
    }
}

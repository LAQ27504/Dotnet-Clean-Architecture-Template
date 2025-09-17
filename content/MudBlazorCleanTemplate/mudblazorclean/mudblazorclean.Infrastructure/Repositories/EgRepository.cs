using mudblazorclean.Application.Interfaces.Repositories;
using mudblazorclean.Domain.Entities;
using mudblazorclean.Infrastructure.Persistence;

namespace mudblazorclean.Infrastructure.Repositories
{
    public class EgRepository : BaseRepository<EgEntity>, IEgRepository
    {
        public EgRepository(AppDbContext context)
            : base(context) { }
    }
}

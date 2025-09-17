using blazorclean.Application.Util;
using blazorclean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blazorclean.Infrastructure.Seeding
{
    public static class UserSeeding
    {
        public static List<User> Seed()
        {
            return new List<User>
            {
                new User
                {
                    Id = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"),
                    Username = "admin",
                    Email = "admin@gmail.com",
                    HashedPassword =
                        "aWb1imnfnUXAdNA5pYu+VUz2DVPdlgYD0IOgjMDr1yDHBVz2z6h/encDgnXBlonT",
                },
            };
        }
    }
}

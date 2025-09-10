using apiclean.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace apiclean.Infrastructure.Configuration
{
    public class EgConfiguration : IEntityTypeConfiguration<EgEntity>
    {
        public void Configure(EntityTypeBuilder<EgEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
        }
    }
}

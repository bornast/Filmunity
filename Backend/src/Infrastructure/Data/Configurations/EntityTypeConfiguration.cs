using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EntityTypeConfiguration : IEntityTypeConfiguration<EntityType>
    {
        public void Configure(EntityTypeBuilder<EntityType> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FilmConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.Property(u => u.Title).IsRequired();
            builder.Property(u => u.Description).IsRequired();
            builder.Property(u => u.Year).IsRequired();
            builder.Property(u => u.Duration).IsRequired();
        }
    }
}

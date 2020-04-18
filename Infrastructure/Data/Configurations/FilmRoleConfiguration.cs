using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FilmRoleConfiguration : IEntityTypeConfiguration<FilmRole>
    {
        public void Configure(EntityTypeBuilder<FilmRole> builder)
        {
            builder.Property(u => u.Name).IsRequired();
        }
    }
}

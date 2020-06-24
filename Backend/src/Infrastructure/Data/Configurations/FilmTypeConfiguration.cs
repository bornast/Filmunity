using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FilmTypeConfiguration : IEntityTypeConfiguration<FilmType>
    {
        public void Configure(EntityTypeBuilder<FilmType> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(u => u.Name).IsRequired();
        }
    }
}

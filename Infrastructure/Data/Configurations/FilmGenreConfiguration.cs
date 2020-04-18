using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FilmGenreConfiguration : IEntityTypeConfiguration<FilmGenre>
    {
        public void Configure(EntityTypeBuilder<FilmGenre> builder)
        {
            builder.HasKey(ur => new { ur.FilmId, ur.GenreId });            

            builder.HasOne(ur => ur.Film)
                .WithMany(r => r.Genres)
                .HasForeignKey(ur => ur.FilmId)
                .IsRequired();

            builder.HasOne(ur => ur.Genre)
                .WithMany(r => r.Films)
                .HasForeignKey(ur => ur.GenreId)
                .IsRequired();
        }
    }
}

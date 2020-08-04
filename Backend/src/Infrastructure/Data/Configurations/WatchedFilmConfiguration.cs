using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{

    public class WatchedFilmConfiguration : IEntityTypeConfiguration<WatchedFilm>
    {
        public void Configure(EntityTypeBuilder<WatchedFilm> builder)
        {
            builder.Ignore(x => x.Id);

            builder.HasKey(x => new { x.UserId, x.FilmId });

            builder.HasOne(x => x.Film)
                .WithMany(x => x.WatchedByUsers)
                .HasForeignKey(x => x.FilmId)
                .IsRequired();

            builder.HasOne(x => x.Film)
                .WithMany(x => x.WatchedByUsers)
                .HasForeignKey(x => x.FilmId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

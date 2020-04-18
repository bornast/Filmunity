using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FilmWatchlistConfiguration : IEntityTypeConfiguration<FilmWatchlist>
    {
        public void Configure(EntityTypeBuilder<FilmWatchlist> builder)
        {
            builder.HasKey(ur => new { ur.WatchlistId, ur.FilmId });

            builder.HasOne(ur => ur.Watchlist)
                .WithMany(r => r.Films)
                .HasForeignKey(ur => ur.WatchlistId)
                .IsRequired();

            builder.HasOne(c => c.Film)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

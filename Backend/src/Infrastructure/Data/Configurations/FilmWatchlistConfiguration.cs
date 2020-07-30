using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class FilmWatchlistConfiguration : IEntityTypeConfiguration<FilmWatchlist>
    {
        public void Configure(EntityTypeBuilder<FilmWatchlist> builder)
        {
            builder.HasKey(x => new { x.WatchlistId, x.FilmId });

            builder.HasOne(x => x.Watchlist)
                .WithMany(x => x.Films)
                .HasForeignKey(x => x.WatchlistId)
                .IsRequired();

            builder.HasOne(x => x.Film)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

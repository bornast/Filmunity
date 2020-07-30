using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.Ignore(x => x.Id);

            builder.HasKey(x => new { x.UserId, x.FilmId });

            builder.Property(x => x.RatingValue).IsRequired();

            builder.HasOne(x => x.Film)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.FilmId)
                .IsRequired();

            builder.HasOne(c => c.Film)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.FilmId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

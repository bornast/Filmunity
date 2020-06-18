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
            builder.HasKey(ur => new { ur.UserId, ur.FilmId });

            builder.Property(u => u.RatingValue).IsRequired();

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

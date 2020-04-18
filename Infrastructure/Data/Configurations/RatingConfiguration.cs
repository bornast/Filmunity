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

            builder.HasOne(ur => ur.Film)
                .WithMany(r => r.Ratings)
                .HasForeignKey(ur => ur.FilmId)
                .IsRequired();

            builder.HasOne(c => c.Film)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

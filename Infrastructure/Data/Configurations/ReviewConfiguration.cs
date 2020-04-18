using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.FilmId });

            builder.Property(u => u.Description).IsRequired();

            builder.HasOne(ur => ur.Film)
                .WithMany(r => r.Reviews)
                .HasForeignKey(ur => ur.FilmId)
                .IsRequired();

            builder.HasOne(c => c.Film)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

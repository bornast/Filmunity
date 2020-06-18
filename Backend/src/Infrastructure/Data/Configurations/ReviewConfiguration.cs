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

            builder.HasOne(x => x.Film)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.FilmId)
                .IsRequired();

            builder.HasOne(x => x.Film)
                .WithMany(x => x.Reviews)
                .HasForeignKey(x => x.FilmId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

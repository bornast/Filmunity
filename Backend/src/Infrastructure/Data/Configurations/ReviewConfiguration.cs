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
            builder.Ignore(x => x.Id);

            builder.HasKey(x => new { x.UserId, x.FilmId, x.Description });

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

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FilmParticipantConfiguration : IEntityTypeConfiguration<FilmParticipant>
    {
        public void Configure(EntityTypeBuilder<FilmParticipant> builder)
        {
            builder.HasKey(ur => new { ur.FilmId, ur.PersonId, ur.FilmRoleId });            

            builder.HasOne(ur => ur.Film)
                .WithMany(r => r.Participants)
                .HasForeignKey(ur => ur.FilmId)
                .IsRequired();

            builder.HasOne(c => c.Person)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

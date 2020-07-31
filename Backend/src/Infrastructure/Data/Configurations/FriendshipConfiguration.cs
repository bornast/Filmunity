using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.Ignore(x => x.Id);

            builder.HasKey(x => new { x.SenderId, x.ReceiverId });

            builder
                .HasOne(u => u.Sender)
                .WithMany(u => u.Receivers)
                .HasForeignKey(u => u.SenderId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder
                .HasOne(u => u.Receiver)
                .WithMany(u => u.Senders)
                .HasForeignKey(u => u.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}




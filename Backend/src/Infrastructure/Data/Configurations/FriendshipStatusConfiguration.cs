using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurations
{
    public class FriendshipStatusConfiguration : IEntityTypeConfiguration<FriendshipStatus>
    {
        public void Configure(EntityTypeBuilder<FriendshipStatus> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}

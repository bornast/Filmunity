using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class FilmunityDataContext : DbContext
    {
        public FilmunityDataContext(DbContextOptions<FilmunityDataContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmunityDataContext).Assembly);
        }

    }
}

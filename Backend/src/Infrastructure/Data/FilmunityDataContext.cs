﻿using Application.Interfaces.Common;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FilmunityDataContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public FilmunityDataContext(DbContextOptions<FilmunityDataContext> options, ICurrentUserService currentUserService): base(options) {
            _currentUserService = currentUserService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FilmunityDataContext).Assembly);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SetTrackableData();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetTrackableData();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetTrackableData()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.Entity is IDateTrackable dateTrackable)
                {
                    var now = DateTime.Now;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            dateTrackable.ModifiedAt = now;
                            break;

                        case EntityState.Added:
                            dateTrackable.CreatedAt = now;
                            break;
                    }
                }

                if (entry.Entity is ITrackable trackable)
                {
                    var userId = _currentUserService.UserId;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            if (userId > 0)
                                trackable.ModifiedByUserId = userId;
                            break;

                        case EntityState.Added:
                            if (userId > 0)
                                trackable.CreatedByUserId = (int)userId;
                            break;
                    }
                }                
            }
        }

    }
}

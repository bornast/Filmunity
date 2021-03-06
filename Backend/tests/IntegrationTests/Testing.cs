﻿using Application.Helpers;
using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System.IO;
using System.Threading.Tasks;
using Api;

namespace IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;        
        private static Checkpoint _checkpoint;
        public static IServiceScopeFactory _scopeFactory;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var currentDirectory = Directory.GetCurrentDirectory().Contains("bin") ? Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().IndexOf("bin")) : Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile($"{currentDirectory}appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(_configuration);

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "Api"));

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] 
                { 
                    "__EFMigrationsHistory",
                    "Gender", 
                    "FilmType", 
                    "Genre",
                    "Country",
                    "FilmRole",
                    "Language",
                    "Roles",
                    "EntityType"
                }
            };
            EnsureDatabase();
        }

        private void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<FilmunityDataContext>();            

            var uow = scope.ServiceProvider.GetService<IUnitOfWork>();
            var hashService = scope.ServiceProvider.GetService<IHashService>();

            context.Database.Migrate();
            Seed.SeedCoreData(uow, hashService);
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("FilmunityDatabase"));
        }

        public static async Task<TEntity> FindAsync<TEntity>(int id) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<FilmunityDataContext>();

            return await context.FindAsync<TEntity>(id);
        }

        public static async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<FilmunityDataContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static TEntity FindOne<TEntity>(BaseSpecification<TEntity> specification) where TEntity: BaseEntity
        {
            using var scope = _scopeFactory.CreateScope();

            var uow = scope.ServiceProvider.GetService<IUnitOfWork>();

            var repo = uow.Repository<TEntity>();

            var result = repo.FindOneAsync(specification).Result;

            return result;
        }

    }
}

﻿using Application.Interfaces;
using Application.Interfaces.Common;
using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FilmunityDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("FilmunityDatabase")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<ICloudUploadService, CloudUploadService>();
        }

    }
}

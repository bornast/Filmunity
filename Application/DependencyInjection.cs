using Application.Extensions;
using Application.Interfaces;
using Application.Services;
using Application.User.Validators;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(IAuthService).Assembly);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthValidatorService, AuthValidatorService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddValidation();
        }

        public static void AddValidation(this IServiceCollection services)
        {
            services.AddTransient<IObjectValidator<UserForLoginDtoValidator>, UserForLoginDtoValidator>();
            services.AddTransient<IObjectValidator<UserForRegistrationDtoValidator>, UserForRegistrationDtoValidator>();
        }

    }
}

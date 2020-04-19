using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Film;
using Application.Services;
using Application.Services.Common;
using Application.Validators;
using Application.Validators.Film;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(IAuthService).Assembly);
            services.AddScoped<IAuthService, AuthService>();            
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IFilmService, FilmService>();
            services.AddValidation();
        }

        public static void AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidatorFactoryService, ValidatorFactoryService>();
            services.AddTransient<IObjectValidator<UserForLoginDtoValidator>, UserForLoginDtoValidator>();
            services.AddTransient<IObjectValidator<UserForRegistrationDtoValidator>, UserForRegistrationDtoValidator>();
            services.AddTransient<IObjectValidator<FilmForCreationDtoValidator>, FilmForCreationDtoValidator>();
            services.AddScoped<IAuthValidatorService, AuthValidatorService>();
            services.AddScoped<IFilmValidatorService, FilmValidatorService>();
        }

    }
}

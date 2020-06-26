using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.EntityType;
using Application.Interfaces.Film;
using Application.Interfaces.Photo;
using Application.Interfaces.Rating;
using Application.Services;
using Application.Services.Common;
using Application.Services.EntityType;
using Application.Services.Photo;
using Application.Services.Rating;
using Application.Validators;
using Application.Validators.Film;
using Application.Validators.Rating;
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
            services.AddScoped<IRatingService, RatingService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IEntityTypeService, EntityTypeService>();
            services.AddValidation();
        }

        public static void AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidatorFactoryService, ValidatorFactoryService>();

            services.AddTransient<IObjectValidator<UserForLoginDtoValidator>, UserForLoginDtoValidator>();
            services.AddTransient<IObjectValidator<UserForRegistrationDtoValidator>, UserForRegistrationDtoValidator>();
            
            services.AddTransient<IObjectValidator<FilmForCreationDtoValidator>, FilmForCreationDtoValidator>();
            services.AddTransient<IObjectValidator<FilmForUpdateDtoValidator>, FilmForUpdateDtoValidator>();
            services.AddScoped<IFilmValidatorService, FilmValidatorService>();

            services.AddTransient<IObjectValidator<RatingDtoValidator>, RatingDtoValidator>();
            services.AddScoped<IRatingValidatorService, RatingValidatorService>();

            services.AddTransient<IObjectValidator<PhotoForCreationDtoValidator>, PhotoForCreationDtoValidator>();
            services.AddScoped<IPhotoValidatorService, PhotoValidatorService>();

            services.AddScoped<IAuthValidatorService, AuthValidatorService>();
        }

    }
}

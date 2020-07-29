using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Country;
using Application.Interfaces.EntityType;
using Application.Interfaces.Film;
using Application.Interfaces.FilmRole;
using Application.Interfaces.Genre;
using Application.Interfaces.Language;
using Application.Interfaces.Person;
using Application.Interfaces.Photo;
using Application.Interfaces.Rating;
using Application.Interfaces.Watchlist;
using Application.Services;
using Application.Services.Common;
using Application.Services.Country;
using Application.Services.EntityType;
using Application.Services.FilmRole;
using Application.Services.Genre;
using Application.Services.Language;
using Application.Services.Person;
using Application.Services.Photo;
using Application.Services.Rating;
using Application.Services.Watchlist;
using Application.Validators;
using Application.Validators.Common;
using Application.Validators.Film;
using Application.Validators.Person;
using Application.Validators.Photo;
using Application.Validators.Rating;
using Application.Validators.Watchlist;
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
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IWatchlistService, WatchlistService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IFilmRoleService, FilmRoleService>();
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

            services.AddTransient<IObjectValidator<TokenDtoValidator>, TokenDtoValidator>();
            services.AddTransient<IObjectValidator<FacebookLoginDtoValidator>, FacebookLoginDtoValidator>();
            services.AddScoped<IAuthValidatorService, AuthValidatorService>();

            services.AddTransient<IObjectValidator<PersonForSaveDtoValidator>, PersonForSaveDtoValidator>();
            services.AddScoped<IPersonValidatorService, PersonValidatorService>();

            services.AddTransient<IObjectValidator<ToggleWatchedDtoValidator>, ToggleWatchedDtoValidator>();
            services.AddTransient<IObjectValidator<WatchlistForCreationDtoValidator>, WatchlistForCreationDtoValidator>();
            services.AddTransient<IObjectValidator<WatchlistForUpdateDtoValidator>, WatchlistForUpdateDtoValidator>();
            services.AddScoped<IWatchlistValidatorService, WatchlistValidatorService>();            
        }

    }
}

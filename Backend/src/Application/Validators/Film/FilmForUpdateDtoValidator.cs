using Application.Dtos.Film;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Linq;

namespace Application.Validators.Film
{
    public class FilmForUpdateDtoValidator : AbstractValidator<FilmForUpdateDto>, IObjectValidator<FilmForUpdateDtoValidator>
    {
        public FilmForUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            
            RuleFor(x => x.Description).NotEmpty();
            
            RuleFor(x => x.CountryId).GreaterThan(0);
            
            RuleFor(x => x.TypeId).GreaterThan(0);
            
            RuleFor(x => x.Year).GreaterThan(1877).LessThanOrEqualTo(DateTime.UtcNow.Year);
            
            RuleFor(x => x.Duration).NotEmpty();
            
            RuleFor(x => x.LanguageId).GreaterThan(0);

            RuleFor(x => x.GenreIds).NotEmpty();

            RuleForEach(x => x.GenreIds).GreaterThan(0);

            RuleFor(x => x.ParticipantsRoles).NotEmpty();
        }
    }
}

using Application.Dtos.Film;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators.Film
{
    public class FilmForCreationDtoValidator : AbstractValidator<FilmForCreationDto>, IObjectValidator<FilmForCreationDtoValidator>
    {
        public FilmForCreationDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            
            RuleFor(x => x.Description).NotEmpty();
            
            RuleFor(x => x.CountryId).GreaterThan(0);
            
            RuleFor(x => x.TypeId).GreaterThan(0);
            
            RuleFor(x => x.Year).GreaterThan(1877);
            
            RuleFor(x => x.Duration).NotEmpty();
            
            RuleFor(x => x.LanguageId).GreaterThan(0);

            RuleFor(x => x.GenreIds).NotEmpty();

            RuleForEach(x => x.GenreIds).GreaterThan(0);

            RuleFor(x => x.ParticipantIds).NotEmpty();

            RuleForEach(x => x.ParticipantIds).GreaterThan(0);
        }
    }
}

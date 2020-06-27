using Application.Dtos.Watchlist;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators.Watchlist
{
    public class WatchlistForUpdateDtoValidator : AbstractValidator<WatchlistForUpdateDto>, IObjectValidator<WatchlistForUpdateDtoValidator>
    {
        public WatchlistForUpdateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Films).NotEmpty();
        }
    }
}

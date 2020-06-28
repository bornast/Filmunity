using Application.Dtos.Watchlist;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators.Watchlist
{
    public class ToggleWatchedDtoValidator : AbstractValidator<ToggleWatchedDto>, IObjectValidator<ToggleWatchedDtoValidator>
    {
        public ToggleWatchedDtoValidator()
        {
            RuleFor(x => x.FilmId).GreaterThan(0);

            RuleFor(x => x.WatchlistId).GreaterThan(0);
        }
    }

}

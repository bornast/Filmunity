using Application.Dtos.Rating;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Rating;
using Application.Specifications.Film;
using Common.Enums;
using Common.Exceptions;
using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Rating
{
    public class RatingValidatorService : BaseValidatorService, IRatingValidatorService
    {

        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public RatingValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow,
            ICurrentUserService currentUserService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task ValidateForRating(int filmId, RatingDto rating)
        {
            Validate(rating);

            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithRatingsSpecification(filmId));
            if (film == null)
                throw new NotFoundException(nameof(Film));

            if (film.Ratings.Any(x => x.UserId == _currentUserService.UserId))
            {
                var filmType = film.TypeId == (int)FilmTypes.Movie ? "Movie" : "Tv show";
                AddValidationError($"{filmType}", $"You already rated this {filmType}!");
            }

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateForUnrating(int filmId)
        {
            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithRatingsSpecification(filmId));
            if (film == null)
                throw new NotFoundException(nameof(Film));

            if (!film.Ratings.Any(x => x.UserId == _currentUserService.UserId))
                throw new BadRequestException();
        }
    }
}

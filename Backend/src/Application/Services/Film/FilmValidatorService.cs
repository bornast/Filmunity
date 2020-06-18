using Application.Dtos.Film;
using Application.Dtos.Rating;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Film;
using Application.Specifications.Film;
using Common.Enums;
using Common.Exceptions;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FilmValidatorService : BaseValidatorService, IFilmValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public FilmValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow,
            ICurrentUserService currentUserService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task ValidateForCreation(FilmForCreationDto filmForCreation)
        {
            Validate(filmForCreation);

            var validationErrors = new Dictionary<string, string>();            

            var language = await _uow.Repository<Language>().FindByIdAsync(filmForCreation.LanguageId);            
            AddValidationErrorIfValueIsNull(language, "Language", $"Id {filmForCreation.LanguageId} not found");

            var genres = await _uow.Repository<Genre>().FindAllByIdAsync(filmForCreation.GenreIds);
            AddValidationErrorIfIdDoesntExist(filmForCreation.GenreIds, genres.Select(x => x.Id).ToList(), "Genre", "Id __id__ not found");            

            var participants = await _uow.Repository<Person>().FindAllByIdAsync(filmForCreation.ParticipantsRoles.Select(x => x.ParticipantId).ToList());
            AddValidationErrorIfIdDoesntExist(filmForCreation.ParticipantsRoles.Select(x => x.ParticipantId).ToList(),
                participants.Select(x => x.Id).ToList(), "Participant", "Id __id__ not found");

            var roles = await _uow.Repository<FilmRole>().FindAllByIdAsync(filmForCreation.ParticipantsRoles.Select(x => x.RoleId).ToList());
            AddValidationErrorIfIdDoesntExist(filmForCreation.ParticipantsRoles.Select(x => x.RoleId).ToList(),
                roles.Select(x => x.Id).ToList(), "Role", "Id __id__ not found");

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateForRating(int filmId, RatingDto rating)
        {               
            Validate(rating);

            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithRatingsSpecification(filmId));            
            if (film == null)
                throw new BadRequestException();
            
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
                throw new BadRequestException();

            if (!film.Ratings.Any(x => x.UserId == _currentUserService.UserId))
                throw new BadRequestException();
        }
    }

}

﻿using Application.Dtos.Film;
using Application.Dtos.Rating;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Film;
using Application.Specifications.Film;
using Application.Specifications.WatchedFilm;
using Application.Specifications.Watchlist;
using Common.Enums;
using Common.Exceptions;
using Common.Libs;
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

        public async Task ValidateForUpdate(int id, FilmForUpdateDto filmForUpdate)
        {
            Validate(filmForUpdate);

            var film = await _uow.Repository<Film>().FindByIdAsync(id);
            if (film == null)
                throw new NotFoundException(nameof(Film));

            ValidateType(filmForUpdate.TypeId);

            await ValidateCountry(filmForUpdate.CountryId);

            await ValidateLanguage(filmForUpdate.LanguageId);

            await ValidateGenre(filmForUpdate.GenreIds);

            await ValidateParticipants(filmForUpdate.ParticipantsRoles.Select(x => x.ParticipantId).ToList());

            await ValidateFilmRoles(filmForUpdate.ParticipantsRoles.Select(x => x.RoleId).ToList());

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateForCreation(FilmForCreationDto filmForCreation)
        {
            Validate(filmForCreation);

            ValidateType(filmForCreation.TypeId);

            await ValidateCountry(filmForCreation.CountryId);

            await ValidateLanguage(filmForCreation.LanguageId);

            await ValidateGenre(filmForCreation.GenreIds);

            await ValidateParticipants(filmForCreation.ParticipantsRoles.Select(x => x.ParticipantId).ToList());

            await ValidateFilmRoles(filmForCreation.ParticipantsRoles.Select(x => x.RoleId).ToList());

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateForDeletion(int id)
        {
            var film = await _uow.Repository<Film>().FindByIdAsync(id);
            AddValidationErrorIfValueIsNull(film, "Film", $"Id {id} not found");

            var watchlist = await _uow.Repository<Domain.Entities.Watchlist>().FindOneAsync(new WatchlistFilterSpecification(id));
            if (watchlist != null)
                AddValidationError("Film", $"Film cannot be delete because it is listed in a existing watchlist!");

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateMarkAsWatched(int filmId)
        {
            var film = await _uow.Repository<Film>().FindByIdAsync(filmId);
            AddValidationErrorIfValueIsNull(film, "Film", $"Id {filmId} not found");

            var watchedFilm = await _uow.Repository<WatchedFilm>().FindOneAsync(new WatchedFilmFilterSpecification(filmId, (int)_currentUserService.UserId));
            if (watchedFilm != null)
                AddValidationError("Film", $"You already marked this film as watched!");

            ThrowValidationErrorsIfNotEmpty();
        }

        #region private methods        

        private async Task ValidateCountry(int countryId)
        {
            var country = await _uow.Repository<Domain.Entities.Language>().FindByIdAsync(countryId);
            AddValidationErrorIfValueIsNull(country, "Country", $"Id {countryId} not found");
        }

        private void ValidateType(int typeId)
        {
            if (!EnumLibrary.GetIntValuesFromEnumType(typeof(FilmTypes)).Contains(typeId))
                AddValidationError("Type", $"Id {typeId} not found!");
        }

        private async Task ValidateLanguage(int languageId)
        {
            var language = await _uow.Repository<Domain.Entities.Language>().FindByIdAsync(languageId);
            AddValidationErrorIfValueIsNull(language, "Language", $"Id {languageId} not found");
        }

        private async Task ValidateGenre(List<int> genreIds)
        {
            var genres = await _uow.Repository<Domain.Entities.Genre>().FindAllByIdAsync(genreIds);
            AddValidationErrorIfIdDoesntExist(genreIds, genres.Select(x => x.Id).ToList(), "Genre", "Id __id__ not found");
        }

        private async Task ValidateParticipants(List<int> participantIds)
        {
            var participants = await _uow.Repository<Domain.Entities.Person>().FindAllByIdAsync(participantIds);
            AddValidationErrorIfIdDoesntExist(participantIds, participants.Select(x => x.Id).ToList(), "Person", "Id __id__ not found");
        }

        private async Task ValidateFilmRoles(List<int> participantIds)
        {
            var filmRoles = await _uow.Repository<Domain.Entities.FilmRole>().FindAllByIdAsync(participantIds);
            AddValidationErrorIfIdDoesntExist(participantIds, filmRoles.Select(x => x.Id).ToList(), "Film Role", "Id __id__ not found");
        }

        #endregion
    }

}

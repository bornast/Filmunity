using Application.Dtos.FilmComment;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.FilmComment;
using Application.Specifications.FilmComment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FilmComment
{
    public class FilmCommentValidatorService : BaseValidatorService, IFilmCommentValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public FilmCommentValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow,
            ICurrentUserService currentUserService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task ValidateForCreation(FilmCommentForCreationDto filmCommentForCreation)
        {
            Validate(filmCommentForCreation);

            await ValidateFilm(filmCommentForCreation.FilmId, filmCommentForCreation.Comment);

            ThrowValidationErrorsIfNotEmpty();
        }

        #region private methods        

        private async Task ValidateFilm(int filmId, string comment)
        {
            var film = await _uow.Repository<Domain.Entities.Film>().FindByIdAsync(filmId);
            AddValidationErrorIfValueIsNull(film, "Film", $"Id {filmId} not found");

            var filmCommentSpecification = new FilmCommentFilterSpecification(filmId, (int)_currentUserService.UserId, comment);
            var filmComment = await _uow.Repository<Domain.Entities.FilmComment>().FindOneAsync(filmCommentSpecification);

            if (filmComment != null)
                AddValidationError("Comment", $"You already posted a same comment!");
        }

        #endregion
    }
}

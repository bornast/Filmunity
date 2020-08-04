using Application.Dtos.Review;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Review;
using Application.Specifications.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Review
{
    public class ReviewValidatorService : BaseValidatorService, IReviewValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public ReviewValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow,
            ICurrentUserService currentUserService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }        

        public async Task ValidateForCreation(ReviewForCreationDto reviewForCreation)
        {
            Validate(reviewForCreation);

            await ValidateFilm(reviewForCreation.FilmId);

            ThrowValidationErrorsIfNotEmpty();
        }

        #region private methods        

        private async Task ValidateFilm(int filmId)
        {
            var film = await _uow.Repository<Domain.Entities.Film>().FindByIdAsync(filmId);
            AddValidationErrorIfValueIsNull(film, "Film", $"Id {filmId} not found");

            var review = await _uow.Repository<Domain.Entities.Review>().FindAsync(new ReviewWithUsersSpecification(filmId));
            
            if (review.Select(x => x.UserId).Any(x => x == (int)_currentUserService.UserId))
                AddValidationError("Review", $"You already reviewed this film!");
        }
        
        #endregion
    }
}

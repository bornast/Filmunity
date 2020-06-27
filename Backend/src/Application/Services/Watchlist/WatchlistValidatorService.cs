using Application.Dtos.Watchlist;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Watchlist;
using Common.Exceptions;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Watchlist
{
    public class WatchlistValidatorService : BaseValidatorService, IWatchlistValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public WatchlistValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow,
            ICurrentUserService currentUserService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task ValidateForCreation(WatchlistForCreationDto watchlistForCreation)
        {
            Validate(watchlistForCreation);

            ValidateSequence(watchlistForCreation.Films.Select(x => x.Sequence).ToList());

            await ValidateFilmIds(watchlistForCreation.Films.Select(x => x.FilmId).ToList());

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateForUpdate(int id, WatchlistForUpdateDto watchlistForUpdate)
        {
            Validate(watchlistForUpdate);

            var watchlist = await _uow.Repository<Domain.Entities.Watchlist>().FindByIdAsync(id);
            if (watchlist == null)
                throw new NotFoundException(nameof(Domain.Entities.Watchlist));

            if (_currentUserService.UserId != watchlist.UserId)
                throw new UnauthorizedException();

            ValidateSequence(watchlistForUpdate.Films.Select(x => x.Sequence).ToList());

            await ValidateFilmIds(watchlistForUpdate.Films.Select(x => x.FilmId).ToList());

            ThrowValidationErrorsIfNotEmpty();
        }

        #region private methods  

        private void ValidateSequence(List<int> sequence)
        {
            // validate if sequence is greater than 0
            if (sequence.Any(x => x <= 0))
                AddValidationError("Sequence", $"Sequence must be greather than 0!");

            // validate if sequence is in order, 
            var sequenceIsConsecutive = !sequence.Select((i, j) => i - j).Distinct().Skip(1).Any();
            if (!sequenceIsConsecutive)
                AddValidationError("Sequence", $"Sequence is not consecutive!");
        }

        private async Task ValidateFilmIds(List<int> filmIds)
        {
            // validate if filmid is sent
            if (filmIds.Any(x => x <= 0))
                AddValidationError("Film", $"Id must be greather than 0!");

            // validate if no duplicates in filmIds
            if (filmIds.Count() != filmIds.Distinct().Count())
                AddValidationError("Film", $"FilmId contains duplicates!");

            //validate if film exists in db
            var films = await _uow.Repository<Film>().FindAllByIdAsync(filmIds);
            AddValidationErrorIfIdDoesntExist(filmIds, films.Select(x => x.Id).ToList(), "Film", "Id __id__ not found");
        }

        #endregion

    }
}

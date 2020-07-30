using Application.Dtos.Watchlist;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Photo;
using Application.Interfaces.Watchlist;
using Application.Specifications.Watchlist;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Watchlist
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhotoService _photoService;

        public WatchlistService(IUnitOfWork uow, 
            IMapper mapper, 
            ICurrentUserService currentUserService, 
            IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
        }

        public async Task<WatchlistForDetailedDto> GetOne(int id)
        {
            var watchlist = await _uow.Repository<Domain.Entities.Watchlist>().FindOneAsync(new WatchlistWithFilmsSpecification(id));

            if (watchlist == null)
                return null;

            var watchlistToReturn = _mapper.Map<WatchlistForDetailedDto>(watchlist);

            await _photoService.IncludeMainPhoto(watchlistToReturn.Films, (int)EntityTypes.Film);

            await _photoService.IncludePhotos(watchlistToReturn, (int)EntityTypes.Watchlist);

            return watchlistToReturn;
        }

        public async Task<IEnumerable<WatchlistForListDto>> GetAll()
        {
            var watchlists = await _uow.Repository<Domain.Entities.Watchlist>().FindAsync();

            var watchlistsToReturn = _mapper.Map<IEnumerable<WatchlistForListDto>>(watchlists);

            await _photoService.IncludeMainPhoto(watchlistsToReturn, (int)EntityTypes.Watchlist);

            return watchlistsToReturn;
        }

        public async Task<WatchlistForDetailedDto> Create(WatchlistForCreationDto watchlistForCreation)
        {
            var watchlist = _mapper.Map<Domain.Entities.Watchlist>(watchlistForCreation);

            watchlist.UserId = (int)_currentUserService.UserId;

            _uow.Repository<Domain.Entities.Watchlist>().Add(watchlist);

            await _uow.SaveAsync();            

            var watchlistToReturn = _mapper.Map<WatchlistForDetailedDto>(watchlist);

            await _photoService.IncludeMainPhoto(watchlistToReturn.Films, (int)EntityTypes.Film);

            return watchlistToReturn;
        }

        public async Task<WatchlistForDetailedDto> Update(int id, WatchlistForUpdateDto watchlistForUpdate)
        {
            var watchlist = await _uow.Repository<Domain.Entities.Watchlist>().FindOneAsync(new WatchlistWithFilmsSpecification(id));

            _mapper.Map(watchlistForUpdate, watchlist);

            await _uow.SaveAsync();

            return await GetOne(watchlist.Id);
        }

        public async Task Delete(int id)
        {
            var watchlist = await _uow.Repository<Domain.Entities.Watchlist>().FindByIdAsync(id);

            if (watchlist == null)
                throw new NotFoundException(nameof(Domain.Entities.Watchlist));

            _uow.Repository<Domain.Entities.Watchlist>().Remove(watchlist);

            await _uow.SaveAsync();
        }

        public async Task MarkAsWatched(ToggleWatchedDto markAsWatched)
        {
            var watchlist = await _uow.Repository<Domain.Entities.Watchlist>().FindOneAsync(new WatchlistWithFilmsSpecification(markAsWatched.WatchlistId));

            var filmToMarkAsWatched = watchlist.Films.FirstOrDefault(x => x.FilmId == markAsWatched.FilmId);

            filmToMarkAsWatched.IsWatched = true;

            await _uow.SaveAsync();
        }

        public async Task MarkAsUnwatched(ToggleWatchedDto markAsUnwatched)
        {
            var watchlist = await _uow.Repository<Domain.Entities.Watchlist>().FindOneAsync(new WatchlistWithFilmsSpecification(markAsUnwatched.WatchlistId));

            var filmToMarkAsUnwatched = watchlist.Films.FirstOrDefault(x => x.FilmId == markAsUnwatched.FilmId);

            filmToMarkAsUnwatched.IsWatched = false;

            await _uow.SaveAsync();
        }
    }
}

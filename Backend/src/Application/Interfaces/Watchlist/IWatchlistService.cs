﻿using Application.Dtos.Watchlist;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Watchlist
{
    public interface IWatchlistService
    {
        Task<IEnumerable<WatchlistForListDto>> GetAll();
        Task<WatchlistForDetailedDto> GetOne(int id);
        Task<WatchlistForDetailedDto> Create(WatchlistForCreationDto watchlistForCreation);
        Task<WatchlistForDetailedDto> Update(int id, WatchlistForUpdateDto watchlistForUpdate);
        Task Delete(int id);
    }
}

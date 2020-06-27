using Application.Dtos.Common;
using Application.Interfaces;
using System;
using System.Collections.Generic;

namespace Application.Dtos.Watchlist
{
    public class WatchlistForCreationDto : IObjectToValidate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<FilmWatchlistDto> Films { get; set; } = new List<FilmWatchlistDto>();
    }
}

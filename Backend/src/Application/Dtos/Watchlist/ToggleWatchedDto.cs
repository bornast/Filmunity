using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Watchlist
{
    public class ToggleWatchedDto : IObjectToValidate
    {
        public int WatchlistId { get; set; }
        public int FilmId { get; set; }
    }
}

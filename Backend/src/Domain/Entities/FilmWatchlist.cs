using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FilmWatchlist
    {
        public Film Film { get; set; }
        public int FilmId { get; set; }
        public Watchlist Watchlist { get; set; }
        public int WatchlistId { get; set; }
    }
}

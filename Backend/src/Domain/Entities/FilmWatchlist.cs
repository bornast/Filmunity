using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FilmWatchlist
    {
        public int Sequence { get; set; }
        public Film Film { get; set; }
        public int FilmId { get; set; }
        public Watchlist Watchlist { get; set; }
        public int WatchlistId { get; set; }
        public bool IsWatched { get; set; }
    }
}

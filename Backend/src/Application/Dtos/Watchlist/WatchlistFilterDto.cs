using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Watchlist
{
    public class WatchlistFilterDto : BaseFilter
    {
        public int? UserId { get; set; }
        public string Title { get; set; }
    }
}

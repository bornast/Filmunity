using Application.Dtos.Watchlist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Watchlist
{
    public class WatchlistFilterPaginatedSpecification : BaseSpecification<Domain.Entities.Watchlist>
    {
        public WatchlistFilterPaginatedSpecification(WatchlistFilterDto watchlistFilter)
            : base(x => (string.IsNullOrWhiteSpace(watchlistFilter.Title) || x.Title.ToLower().Contains(watchlistFilter.Title.ToLower()))
            && (watchlistFilter.UserId == null || x.UserId == watchlistFilter.UserId))
        {
            if (watchlistFilter.OrderByDescending == nameof(Domain.Entities.Watchlist.CreatedAt))
                ApplyOrderByDescending(x => x.CreatedAt);

            ApplyPaging(watchlistFilter.Skip, watchlistFilter.Take, watchlistFilter.PageNumber);
        }
    }
}

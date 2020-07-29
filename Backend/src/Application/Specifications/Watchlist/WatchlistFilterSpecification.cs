using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Specifications.Watchlist
{
    public class WatchlistFilterSpecification : BaseSpecification<Domain.Entities.Watchlist>
    {
        public WatchlistFilterSpecification(int filmId)
            : base(x => x.Films.Select(x => x.FilmId).Contains(filmId))
        {
            AddInclude($"{nameof(Domain.Entities.Watchlist.Films)}");
        }
    }
}

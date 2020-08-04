namespace Application.Specifications.Watchlist
{
    public class WatchlistWithFilmsSpecification : BaseSpecification<Domain.Entities.Watchlist>
    {
        public WatchlistWithFilmsSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude($"{nameof(Domain.Entities.Watchlist.Films)}.{nameof(Domain.Entities.FilmWatchlist.Film)}.{nameof(Domain.Entities.Film.WatchedByUsers)}");
        }
    }
}

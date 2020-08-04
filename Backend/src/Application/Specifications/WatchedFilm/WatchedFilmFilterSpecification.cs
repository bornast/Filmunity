using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.WatchedFilm
{
    public class WatchedFilmFilterSpecification : BaseSpecification<Domain.Entities.WatchedFilm>
    {
        public WatchedFilmFilterSpecification(int filmId, int userId)
            : base(x => x.FilmId == filmId && x.UserId == userId)
        {

        }
    }
}

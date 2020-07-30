using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Rating
{
    public class RatingFromUserSpecification : BaseSpecification<Domain.Entities.Rating>
    {
        public RatingFromUserSpecification(int userId, int filmId)
            : base(x => x.UserId == userId && x.FilmId == filmId)
        {
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Review
{
    public class ReviewWithUsersSpecification : BaseSpecification<Domain.Entities.Review>
    {
        public ReviewWithUsersSpecification(int filmId)
            : base(x => x.FilmId == filmId)
        {
            AddInclude($"{nameof(Domain.Entities.Review.User)}");
        }
    }
}

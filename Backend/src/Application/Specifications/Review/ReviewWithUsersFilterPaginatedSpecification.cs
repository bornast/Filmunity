using Application.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Review
{
    public class ReviewWithUsersFilterPaginatedSpecification : BaseSpecification<Domain.Entities.Review>
    {
        public ReviewWithUsersFilterPaginatedSpecification(ReviewFilterDto reviewFilter)
            : base(x => reviewFilter.FilmId == null || x.FilmId == reviewFilter.FilmId)
        {
            if (reviewFilter.OrderByDescending == nameof(Domain.Entities.Review.CreatedAt))
                ApplyOrderByDescending(x => x.CreatedAt);

            ApplyPaging(reviewFilter.Skip, reviewFilter.Take, reviewFilter.PageNumber);

            AddInclude($"{nameof(Domain.Entities.Review.User)}");
        }
    }

}

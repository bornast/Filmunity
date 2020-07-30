using Application.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Review
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewForListDto>> GetAll(ReviewFilterDto reviewFilter);
        Task Create(ReviewForCreationDto reviewForCreation);
    }
}

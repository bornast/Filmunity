using Application.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Review
{
    public interface IReviewValidatorService
    {
        Task ValidateForCreation(ReviewForCreationDto reviewForCreation);
    }
}

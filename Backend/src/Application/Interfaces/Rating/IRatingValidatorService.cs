using Application.Dtos.Rating;
using System.Threading.Tasks;

namespace Application.Interfaces.Rating
{
    public interface IRatingValidatorService
    {
        Task ValidateForRating(int filmId, RatingDto rating);
        Task ValidateForUnrating(int filmId);
    }
}

using Application.Dtos.Rating;
using System.Threading.Tasks;

namespace Application.Interfaces.Rating
{
    public interface IRatingService
    {
        Task<RatingDto> GetLoggedUserRating(int filmId);
        Task Rate(int id, RatingDto rating);
        Task Unrate(int id);
    }
}

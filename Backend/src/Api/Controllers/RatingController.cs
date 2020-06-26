using Application.Dtos.Film;
using Application.Interfaces.Film;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.ActionFilters;
using Application.Dtos.Rating;
using Application.Interfaces.Rating;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;
        private readonly IRatingValidatorService _ratingValidatorService;

        public RatingController(IRatingService ratingService, IRatingValidatorService ratingValidatorService)
        {
            _ratingService = ratingService;
            _ratingValidatorService = ratingValidatorService;
        }

        [HttpPost("rate/{filmId}")]
        public async Task<IActionResult> Rate(int filmId, RatingDto rating)
        {
            await _ratingValidatorService.ValidateForRating(filmId, rating);

            await _ratingService.Rate(filmId, rating);

            return Ok();
        }

        [HttpPost("unrate/{filmId}")]
        public async Task<IActionResult> Unrate(int filmId)
        {
            await _ratingValidatorService.ValidateForUnrating(filmId);

            await _ratingService.Unrate(filmId);

            return Ok();
        }

    }
}

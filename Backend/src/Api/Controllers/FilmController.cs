using Application.Dtos.Film;
using Application.Interfaces.Film;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.ActionFilters;
using Application.Dtos.Rating;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;
        private readonly IFilmValidatorService _filmValidatorService;

        public FilmController(IFilmService filmService, IFilmValidatorService filmValidatorService)
        {
            _filmService = filmService;
            _filmValidatorService = filmValidatorService;
        }        
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilmFilterDto filmFilter)
        {
            return Ok(await _filmService.GetAll(filmFilter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var movie = await _filmService.GetOne(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FilmForCreationDto filmForCreation)
        {
            await _filmValidatorService.ValidateForCreation(filmForCreation);

            var film = await _filmService.Create(filmForCreation);

            // TODO: return CreatedAtRoute
            // return CreatedAtRoute("GetUser", new { controller = "Users", id = userToCreate.Id }, userToReturn);
            return Ok(film);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FilmForUpdateDto filmForUpdate)
        {
            await _filmValidatorService.ValidateForUpdate(id, filmForUpdate);

            var film = await _filmService.Update(id, filmForUpdate);

            return Ok(film);
        }

        [HttpPost("rate/{id}")]
        public async Task<IActionResult> Rate(int id, RatingDto rating)
        {
            await _filmValidatorService.ValidateForRating(id, rating);

            await _filmService.Rate(id, rating);

            return Ok();
        }

        [HttpPost("unrate/{id}")]
        public async Task<IActionResult> Unrate(int id)
        {
            await _filmValidatorService.ValidateForUnrating(id);

            await _filmService.Unrate(id);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _filmService.Delete(id);

            return Ok();
        }

    }
}
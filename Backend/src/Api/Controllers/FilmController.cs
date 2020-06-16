using Application.Dtos.Film;
using Application.Interfaces.Film;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.ActionFilters;

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

        // TODO: add filterModelFromQuery
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _filmService.GetAll());
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

    }
}
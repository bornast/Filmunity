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

        [HttpGet("{id}", Name = "GetFilm")]
        public async Task<IActionResult> GetOne(int id)
        {
            var film = await _filmService.GetOne(id);

            if (film == null)
                return NotFound();

            return Ok(film);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
        [HttpPost]
        public async Task<IActionResult> Create(FilmForCreationDto filmForCreation)
        {
            await _filmValidatorService.ValidateForCreation(filmForCreation);

            var film = await _filmService.Create(filmForCreation);

            return CreatedAtRoute("GetFilm", new { controller = "Film", id = film.Id }, film);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FilmForUpdateDto filmForUpdate)
        {
            await _filmValidatorService.ValidateForUpdate(id, filmForUpdate);

            var film = await _filmService.Update(id, filmForUpdate);

            return Ok(film);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _filmValidatorService.ValidateForDeletion(id);

            await _filmService.Delete(id);

            return Ok();
        }

        [HttpGet("recordNames")]
        public async Task<IActionResult> GetRecordNames()
        {
            return Ok(await _filmService.GetRecordNames());
        }

    }
}
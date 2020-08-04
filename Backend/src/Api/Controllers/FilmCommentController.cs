using Application.Dtos.FilmComment;
using Application.Interfaces.FilmComment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmCommentController : ControllerBase
    {
        private readonly IFilmCommentService _filmCommentService;
        private readonly IFilmCommentValidatorService _filmCommentValidatorService;

        public FilmCommentController(IFilmCommentService filmCommentService, IFilmCommentValidatorService filmCommentValidatorService)
        {
            _filmCommentService = filmCommentService;
            _filmCommentValidatorService = filmCommentValidatorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FilmCommentFilterDto filmCommentFilter)
        {
            return Ok(await _filmCommentService.GetAll(filmCommentFilter));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(FilmCommentForCreationDto filmCommentForCreation)
        {
            await _filmCommentValidatorService.ValidateForCreation(filmCommentForCreation);

            await _filmCommentService.Create(filmCommentForCreation);

            return Ok();
        }

    }
}

using Application.Dtos.Film;
using Application.Dtos.User;
using Application.Interfaces;
using Application.Interfaces.Film;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ActionFilters;

namespace Web.Controllers
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

        [HttpPost]
        public async Task<IActionResult> Create(FilmForCreationDto filmForCreation)
        {
            _filmValidatorService.ValidateForCreation(filmForCreation);

            var film = await _filmService.Create(filmForCreation);

            // TODO: return CreatedAtRoute
            return Ok(film);
        }        

    }
}

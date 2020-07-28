using Application.Interfaces.FilmRole;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmRoleController : ControllerBase
    {
        private readonly IFilmRoleService _filmRoleService;

        public FilmRoleController(IFilmRoleService filmRoleService)
        {
            _filmRoleService = filmRoleService;
        }

        [HttpGet("recordNames")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _filmRoleService.GetRecordNames());
        }
    }
}

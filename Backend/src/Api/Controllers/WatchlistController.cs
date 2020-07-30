using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Api.ActionFilters;
using Application.Interfaces.Watchlist;
using Application.Dtos.Watchlist;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistService _watchlistService;
        private readonly IWatchlistValidatorService _watchlistValidatorService;

        public WatchlistController(IWatchlistService watchlistService, IWatchlistValidatorService watchlistValidatorService)
        {
            _watchlistService = watchlistService;
            _watchlistValidatorService = watchlistValidatorService;
        }        
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] WatchlistFilterDto watchlistFilter)
        {
            return Ok(await _watchlistService.GetAll(watchlistFilter));
        }

        [HttpGet("{id}", Name = "GetWatchlist")]
        public async Task<IActionResult> GetOne(int id)
        {
            var watchlist = await _watchlistService.GetOne(id);

            if (watchlist == null)
                return NotFound();

            return Ok(watchlist);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator, Roles.User)]
        [HttpPost]        
        public async Task<IActionResult> Create(WatchlistForCreationDto watchlistForCreation)
        {
            await _watchlistValidatorService.ValidateForCreation(watchlistForCreation);

            var watchlist = await _watchlistService.Create(watchlistForCreation);

            return CreatedAtRoute("GetWatchlist", new { controller = "Watchlist", id = watchlist.Id }, watchlist);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator, Roles.User)]
        [HttpPut("{id}")]        
        public async Task<IActionResult> Update(int id, WatchlistForUpdateDto watchlistForUpdate)
        {
            await _watchlistValidatorService.ValidateForUpdate(id, watchlistForUpdate);

            var watchlist = await _watchlistService.Update(id, watchlistForUpdate);

            return Ok(watchlist);
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator, Roles.User)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _watchlistService.Delete(id);

            return Ok();
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator, Roles.User)]
        [HttpPost("markAsWatched")]
        public async Task<IActionResult> MarkAsWatched(ToggleWatchedDto markAsWatched)
        {
            await _watchlistValidatorService.ValidateForMarkingAsWatched(markAsWatched);

            await _watchlistService.MarkAsWatched(markAsWatched);

            return Ok();
        }

        [AuthorizeRoles(Roles.Admin, Roles.Moderator, Roles.User)]
        [HttpPost("markAsUnwatched")]
        public async Task<IActionResult> MarkAsUnwatched(ToggleWatchedDto markAsUnwatched)
        {
            await _watchlistValidatorService.ValidateForMarkingAsUnwatched(markAsUnwatched);

            await _watchlistService.MarkAsUnwatched(markAsUnwatched);

            return Ok();
        }

    }
}
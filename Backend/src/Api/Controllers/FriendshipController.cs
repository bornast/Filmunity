using Application.Dtos.Friendship;
using Application.Interfaces.Friendship;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IFriendshipValidatorService _friendshipValidatorService;

        public FriendshipController(IFriendshipService friendshipService, IFriendshipValidatorService friendshipValidatorService)
        {
            _friendshipService = friendshipService;
            _friendshipValidatorService = friendshipValidatorService;
        }

        [HttpGet("getAllFriendRequests")]
        public async Task<IActionResult> GetAllFriendRequests()
        {
            return Ok(await _friendshipService.GetAllFriendRequests());
        }

        [HttpGet("getAllFriends")]
        public async Task<IActionResult> GetAllFriends([FromQuery] FriendshipFilterDto friendshipFilter)
        {
            return Ok(await _friendshipService.GetAllFriends(friendshipFilter));
        }

        [Authorize]
        [HttpPost("sendFriendRequest/{userId}")]
        public async Task<IActionResult> SendFriendRequest(int userId)
        {
            await _friendshipValidatorService.ValidateFriendRequest(userId);

            await _friendshipService.SendFriendRequest(userId);

            return Ok();
        }

        [Authorize]
        [HttpPost("acceptFriendRequest/{userId}")]
        public async Task<IActionResult> AcceptFriendRequest(int userId)
        {
            await _friendshipValidatorService.ValidateAcceptOrDeclineFriendRequest(userId);

            await _friendshipService.AcceptFriendRequest(userId);

            return Ok();
        }

        [Authorize]
        [HttpPost("declineFriendRequest/{userId}")]
        public async Task<IActionResult> DeclineFriendRequest(int userId)
        {
            await _friendshipValidatorService.ValidateAcceptOrDeclineFriendRequest(userId);

            await _friendshipService.DeclineFriendRequest(userId);

            return Ok();
        }

        [Authorize]
        [HttpGet("getFriendshipStatus/{userId}")]
        public async Task<IActionResult> GetFriendshipStatus(int userId)
        {
            return Ok(await _friendshipService.GetFriendShipStatus(userId));
        }
    }

}

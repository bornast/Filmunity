using Api.ActionFilters;
using Application.Dtos.User;
using Application.Interfaces.User;
using Common.Enums;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserValidatorService _userValidatorService;

        public UserController(IUserService userService, IUserValidatorService userValidatorService)
        {
            _userService = userService;
            _userValidatorService = userValidatorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserFilterDto userFilter)
        {
            return Ok(await _userService.GetAll(userFilter));
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetOne(int id)
        {
            var user = await _userService.GetOne(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }        

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserForUpdateDto userForUpdate)
        {
            await _userValidatorService.ValidateForUpdate(id, userForUpdate);

            var user = await _userService.Update(id, userForUpdate);

            return Ok(user);
        }

        [AuthorizeRoles(Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);

            return Ok();
        }        

    }
}

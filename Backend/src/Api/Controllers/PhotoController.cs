using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Api.ActionFilters;
using System.Threading.Tasks;
using Application.Dtos.Photo;
using Application.Interfaces.Photo;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles(Roles.Admin, Roles.Moderator)]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IPhotoValidatorService _photoValidatorService;

        public PhotoController(IPhotoService photoService, IPhotoValidatorService photoValidatorService)
        {
            _photoService = photoService;
            _photoValidatorService = photoValidatorService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto([FromForm] PhotoForCreationDto photoForCreation)
        {
            await _photoValidatorService.ValidateForUpload(photoForCreation);

            await _photoService.Upload(photoForCreation);

            return Ok();
        }

    }
}

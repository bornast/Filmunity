using Common.Enums;
using Microsoft.AspNetCore.Mvc;
using Api.ActionFilters;
using System.Threading.Tasks;
using Application.Dtos.Photo;
using Application.Interfaces.Photo;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            var photo = await _photoService.Upload(photoForCreation);

            return Ok(photo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            await _photoValidatorService.ValidateForDeletion(id);

            await _photoService.Delete(id);

            return Ok();
        }

        [HttpPost("setMain/{id}")]
        public async Task<IActionResult> SetMainPhoto(int id)
        {
            await _photoValidatorService.ValidateForSetMain(id);

            await _photoService.SetMain(id);

            return Ok();
        }
    }
}

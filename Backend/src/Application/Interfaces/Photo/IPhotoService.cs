using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Photo
{
    public interface IPhotoService
    {
        Task<PhotoForDetailedDto> Upload(PhotoForCreationDto photoForCreation);
        Task<IEnumerable<PhotoForDetailedDto>> GetEntityPhotos(int entityTypeId, int entityId);
        Task IncludePhotos(IPhotoUploadable entity, int entityTypeId);
        Task IncludeMainPhoto(IMainPhotoUploadable entity, int entityTypeId);
        Task IncludeMainPhoto(IEnumerable<IMainPhotoUploadable> entities, int entityTypeId);        
    }
}

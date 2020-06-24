using Application.Dtos.Photo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Photo
{
    public interface IPhotoService
    {
        Task Upload(PhotoForCreationDto photoForCreation);
        Task<IEnumerable<PhotoForDetailedDto>> GetEntityPhotos(int entityTypeId, int entityId);
    }
}

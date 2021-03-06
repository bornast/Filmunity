﻿using Application.Dtos.Photo;
using System.Threading.Tasks;

namespace Application.Interfaces.Photo
{
    public interface IPhotoValidatorService
    {
        Task ValidateForUpload(PhotoForCreationDto photoForCreation);
        Task ValidateForDeletion(int photoId);
        Task ValidateForSetMain(int photoId);
    }
}

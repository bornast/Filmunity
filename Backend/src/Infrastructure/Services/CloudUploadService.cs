using Application.Interfaces.Common;
using Application.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class CloudUploadService : ICloudUploadService
    {
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public CloudUploadService(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public PhotoCloudUploadResult UploadPhotoToCloud(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();

            using (var stream = photo.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(photo.Name, stream),
                };

                uploadResult = _cloudinary.Upload(uploadParams);
            }

            var result = new PhotoCloudUploadResult
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString()
            };

            return result;
        }
    }
}

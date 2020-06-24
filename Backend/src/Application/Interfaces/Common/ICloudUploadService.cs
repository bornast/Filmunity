using Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Common
{
    public interface ICloudUploadService
    {
        public PhotoCloudUploadResult UploadPhotoToCloud(IFormFile photo);
    }
}

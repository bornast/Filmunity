using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Photo
{
    public class PhotoForCreationDto : IObjectToValidate
    {
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
    }
}

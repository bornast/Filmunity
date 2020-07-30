using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Review
{
    public class ReviewUserDto : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
    }
}

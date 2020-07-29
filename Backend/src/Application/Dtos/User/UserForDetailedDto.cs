using Application.Dtos.Common;
using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserForDetailedDto : IPhotoUploadable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Interests { get; set; }
        public IEnumerable<RecordNameDto> Roles { get; set; } = new List<RecordNameDto>();
        public PhotoForDetailedDto MainPhoto { get; set; }
        public IEnumerable<PhotoForDetailedDto> Photos { get; set; } = new List<PhotoForDetailedDto>();
    }
}

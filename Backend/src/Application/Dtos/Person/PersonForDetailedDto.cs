using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;

namespace Application.Dtos.Person
{
    public class PersonForDetailedDto : IPhotoUploadable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public int GenderId { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
        public IEnumerable<PhotoForDetailedDto> Photos { get; set; }
    }
}

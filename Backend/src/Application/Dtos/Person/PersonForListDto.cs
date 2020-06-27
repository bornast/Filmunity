using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;

namespace Application.Dtos.Person
{
    public class PersonForListDto : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
    }
}

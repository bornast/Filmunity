using Application.Dtos.Photo;
using System.Collections.Generic;

namespace Application.Interfaces.Common
{
    public interface IPhotoUploadable
    {
        public int Id { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
        public IEnumerable<PhotoForDetailedDto> Photos { get; set; }
    }
}

using Application.Dtos.Photo;

namespace Application.Interfaces.Common
{
    public interface IMainPhotoUploadable
    {
        public int Id { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
    }
}

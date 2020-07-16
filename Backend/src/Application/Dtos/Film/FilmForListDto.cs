using Application.Dtos.Photo;
using Application.Interfaces.Common;

namespace Application.Dtos.Film
{
    public class FilmForListDto : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
        public float Rating { get; set; }
    }
}

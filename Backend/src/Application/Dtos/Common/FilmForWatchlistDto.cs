using Application.Dtos.Photo;
using Application.Interfaces.Common;

namespace Application.Dtos.Common
{
    public class FilmForWatchlistDto : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
        public bool? IsWatched { get; set; }
        public int Sequence { get; set; }
    }
}

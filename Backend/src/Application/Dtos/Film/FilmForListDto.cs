using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System.Collections.Generic;

namespace Application.Dtos.Film
{
    public class FilmForListDto : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
        public float Rating { get; set; }
        public float ImdbRating { get; set; }
        public List<string> Genres { get; set; } = new List<string>();
    }
}

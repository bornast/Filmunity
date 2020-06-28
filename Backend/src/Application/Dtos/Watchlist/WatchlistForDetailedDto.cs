using Application.Dtos.Common;
using Application.Dtos.Film;
using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;

namespace Application.Dtos.Watchlist
{
    public class WatchlistForDetailedDto : IPhotoUploadable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public ICollection<FilmForWatchlistDto> Films { get; set; } = new List<FilmForWatchlistDto>();
        public PhotoForDetailedDto MainPhoto { get; set; }
        public IEnumerable<PhotoForDetailedDto> Photos { get; set; } = new List<PhotoForDetailedDto>();
    }
}

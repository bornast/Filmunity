using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;

namespace Application.Dtos.Watchlist
{
    public class WatchlistForListDto : IMainPhotoUploadable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
        public PhotoForDetailedDto MainPhoto { get; set; }
    }
}

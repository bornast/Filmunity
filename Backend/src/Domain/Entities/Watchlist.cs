using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Watchlist : BaseEntity, IDateTrackable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public ICollection<FilmWatchlist> Films { get; set; } = new List<FilmWatchlist>();
    }
}

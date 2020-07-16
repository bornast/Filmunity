using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Film : BaseEntity, ITrackable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public FilmType Type { get; set; }
        public int TypeId { get; set; }
        public int Year { get; set; }
        public string Duration { get; set; }
        public Language Language { get; set; }
        public int LanguageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public User CreatedByUser { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public User ModifiedByUser { get; set; }
        public int? ModifiedByUserId { get; set; }
        public float Rating { get; set; }
        public ICollection<FilmGenre> Genres { get; set; } = new List<FilmGenre>();
        public ICollection<FilmParticipant> Participants { get; set; } = new List<FilmParticipant>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WatchedFilm : BaseEntity
    {
        public Film Film { get; set; }
        public int FilmId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}

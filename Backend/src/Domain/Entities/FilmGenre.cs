using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FilmGenre
    {
        public Film Film { get; set; }
        public int FilmId { get; set; }
        public Genre Genre { get; set; }
        public int GenreId { get; set; }
    }
}

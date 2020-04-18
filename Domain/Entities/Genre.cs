using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<FilmGenre> Films { get; set; } = new List<FilmGenre>();
    }
}

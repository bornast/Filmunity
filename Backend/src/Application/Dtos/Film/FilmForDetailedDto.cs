using Application.Dtos.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Film
{
    public class FilmForDetailedDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PhotoForDetailedDto MainPhoto { get; set; }
        public IEnumerable<PhotoForDetailedDto> Photos { get; set; }
    }
}

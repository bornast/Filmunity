using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Film
{
    public class FilmFilterDto : BaseFilter
    {
        public int? FilmType { get; set; }
        public string Title { get; set; }
        public List<int> Ids { get; set; } = new List<int>();
        public int? GenreId { get; set; }
    }
}

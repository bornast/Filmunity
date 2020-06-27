using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Film
{
    public class FilmFilterDto : BaseFilter
    {
        public string Title { get; set; }
        public List<int> Ids { get; set; } = new List<int>();
    }
}

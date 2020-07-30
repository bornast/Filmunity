using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Review
{
    public class ReviewFilterDto : BaseFilter
    {
        public int? FilmId { get; set; }
    }
}

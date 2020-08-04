using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.FilmComment
{
    public class FilmCommentFilterDto : BaseFilter
    {
        public int? FilmId { get; set; }
    }
}

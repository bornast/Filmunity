using Application.Dtos.Review;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.FilmComment
{
    public class FilmCommentForListDto
    {
        public ReviewUserDto User { get; set; } = new ReviewUserDto();
        public string Comment { get; set; }
    }
}

using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.FilmComment
{
    public class FilmCommentForCreationDto : IObjectToValidate
    {
        public int FilmId { get; set; }
        public string Comment { get; set; }
    }
}

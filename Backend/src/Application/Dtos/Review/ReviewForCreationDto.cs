using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Review
{
    public class ReviewForCreationDto : IObjectToValidate
    {
        public int FilmId { get; set; }
        public string Comment { get; set; }
    }
}

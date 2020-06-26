using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Rating
{
    public class RatingDto : IObjectToValidate
    {
        public int FilmId { get; set; }
        public int Rating { get; set; }
    }
}

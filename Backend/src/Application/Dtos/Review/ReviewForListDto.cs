using Application.Dtos.Photo;
using Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Review
{
    public class ReviewForListDto
    {
        public ReviewUserDto User { get; set; } = new ReviewUserDto();
        public string Comment { get; set; }
    }    
}

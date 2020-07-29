using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Photo
{
    public class PhotoForDeletionDto : IObjectToValidate
    {
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
    }
}

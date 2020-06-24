using Application.Dtos.Photo;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class PhotoMappings : Profile
    {
        public PhotoMappings()
        {
            CreateMap<PhotoForCreationDto, Photo>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow));

            CreateMap<Photo, PhotoForDetailedDto>();
        }
    }

}

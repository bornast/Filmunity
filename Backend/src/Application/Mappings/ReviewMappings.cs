using Application.Dtos.Review;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class ReviewMappings : Profile
    {
        public ReviewMappings()
        {
            CreateMap<Domain.Entities.Review, ReviewForListDto>()
                .ForMember(x => x.Comment, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.User, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.User.Id = src.UserId;
                    dest.User.Name = src.User.FirstName + " " + src.User.LastName;
                    dest.User.Username = src.User.Username;
                });

            CreateMap<ReviewForCreationDto, Domain.Entities.Review>()
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Comment));
        }
    }
}
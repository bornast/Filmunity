using Application.Dtos.User;
using Application.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<FacebookUser, UserForRegistrationDto>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Email))
                .ForMember(x => x.Password, opt => opt.MapFrom(x => ""));
        }
    }
}

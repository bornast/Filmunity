using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserForRegistrationDto, User>();
                
        }
    }
}

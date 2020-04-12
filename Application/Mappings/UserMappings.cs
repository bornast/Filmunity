using Application.Dtos.User;
using AutoMapper;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserForRegistrationDto, Domain.Entities.User>();
                
        }
    }
}

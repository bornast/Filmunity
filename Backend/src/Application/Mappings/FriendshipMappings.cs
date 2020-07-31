using Application.Dtos.Friendship;
using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{    
    public class FriendshipMappings : Profile
    {
        public FriendshipMappings()
        {
            CreateMap<Friendship, FriendRequestForListDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Sender.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Sender.Username))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Sender.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Sender.LastName))
                .ForMember(x => x.Interests, opt => opt.MapFrom(x => x.Sender.Interests));

            CreateMap<Friendship, FriendDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Receiver.Id))
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.Receiver.Username))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Receiver.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Receiver.LastName))
                .ForMember(x => x.Interests, opt => opt.MapFrom(x => x.Receiver.Interests));
        }
    }

}

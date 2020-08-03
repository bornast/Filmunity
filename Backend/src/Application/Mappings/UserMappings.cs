using Application.Dtos.Common;
using Application.Dtos.User;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using System.Linq;

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

            CreateMap<TwitterUser, UserForRegistrationDto>()
                .ForMember(x => x.Username, opt => opt.MapFrom(x => x.ScreenName))
                .ForMember(x => x.Email, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.Password, opt => opt.MapFrom(x => ""));

            CreateMap<User, UserForListDto>();

            CreateMap<User, UserForDetailedDto>()
                .ForMember(x => x.Roles, opt => 
                opt.MapFrom(x => x.Roles.Select(x => new RecordNameDto { Id = x.RoleId, Name = x.Role.Name })));

            CreateMap<UserForUpdateDto, User>()
                .AfterMap((src, dest) =>
                {
                    HandleRoles(src, dest);
                });
        }

        private void HandleRoles(UserForUpdateDto src, User dest)
        {
            // remove roles
            var rolesToRemove = dest.Roles.Where(x => !src.RoleIds.Contains(x.RoleId)).ToList();

            foreach (var roleToRemove in rolesToRemove)
                dest.Roles.Remove(roleToRemove);

            // add roles
            var rolesToAdd = src.RoleIds.Where(id => !dest.Roles.Any(x => x.RoleId == id)).ToList()
            .Select(id => new UserRole { RoleId = id });

            foreach (var roleToAdd in rolesToAdd)
                dest.Roles.Add(roleToAdd);
        }
    }
}

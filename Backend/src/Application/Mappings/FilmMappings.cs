using Application.Dtos.Film;
using AutoMapper;
using Domain.Entities;
using System.Linq;

namespace Application.Mappings
{
    public class FilmMappings : Profile
    {
        public FilmMappings()
        {
            CreateMap<FilmForCreationDto, Film>()
                .ForMember(x => x.Genres, 
                    opt => opt.MapFrom(x => x.GenreIds.Select(x => new FilmGenre { GenreId = x }) ))
                .ForMember(x => x.Pariticpants,
                    opt => opt.MapFrom(x => x.ParticipantsRoles.Select(x => 
                        new FilmParticipant { PersonId = x.ParticipantId, FilmRoleId = x.RoleId })));

            CreateMap<Film, FilmForDetailedDto>();

            CreateMap<Film, FilmForListDto>();            
        }
    }
}

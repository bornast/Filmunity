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
                opt => opt.MapFrom(x => x.GenreIds.Select(x => new FilmGenre { GenreId = x }) ));

            CreateMap<Film, FilmForDetailedDto>();
        }
    }
}

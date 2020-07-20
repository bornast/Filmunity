using Application.Dtos.Common;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GenreMappings : Profile
    {
        public GenreMappings()
        {
            CreateMap<Genre, RecordNameDto>();            
        }
    }
}

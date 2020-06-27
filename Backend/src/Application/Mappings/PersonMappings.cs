using Application.Dtos.Person;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class PersonMappings : Profile
    {
        public PersonMappings()
        {
            CreateMap<Person, PersonForDetailedDto>();

            CreateMap<Person, PersonForListDto>();

            CreateMap<PersonForSaveDto, Person>();
        }
    }
}

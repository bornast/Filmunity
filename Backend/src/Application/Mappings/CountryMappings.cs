using Application.Dtos.Common;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class CountryMappings : Profile
    {
        public CountryMappings()
        {
            CreateMap<Country, RecordNameDto>();
        }
    }
}

using Application.Dtos.Common;
using Application.Interfaces;
using Application.Interfaces.Country;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Country
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecordNameDto>> GetRecordNames()
        {
            var countries = await _uow.Repository<Domain.Entities.Country>().FindAsync();

            var countriesToReturn = _mapper.Map<IEnumerable<RecordNameDto>>(countries);

            return countriesToReturn;
        }
    }
}

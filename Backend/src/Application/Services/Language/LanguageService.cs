using Application.Dtos.Common;
using Application.Interfaces;
using Application.Interfaces.Language;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Language
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LanguageService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecordNameDto>> GetRecordNames()
        {
            var languages = await _uow.Repository<Domain.Entities.Language>().FindAsync();

            var languagesToReturn = _mapper.Map<IEnumerable<RecordNameDto>>(languages);

            return languagesToReturn;
        }
    }
}

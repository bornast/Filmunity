using Application.Dtos.Common;
using Application.Interfaces;
using Application.Interfaces.Genre;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Genre
{
    public class GenreService : IGenreService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GenreService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecordNameDto>> GetRecordNames()
        {
            var genres = await _uow.Repository<Domain.Entities.Genre>().FindAsync();

            var genresToReturn = _mapper.Map<IEnumerable<RecordNameDto>>(genres);

            return genresToReturn;
        }
    }
}

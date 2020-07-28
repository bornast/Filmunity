using Application.Dtos.Common;
using Application.Interfaces;
using Application.Interfaces.FilmRole;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FilmRole
{
    public class FilmRoleService : IFilmRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FilmRoleService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecordNameDto>> GetRecordNames()
        {
            var filmRoles = await _uow.Repository<Domain.Entities.FilmRole>().FindAsync();

            var filmRolesToReturn = _mapper.Map<IEnumerable<RecordNameDto>>(filmRoles);

            return filmRolesToReturn;
        }
    }
}

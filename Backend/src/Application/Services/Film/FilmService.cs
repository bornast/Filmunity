using Application.Dtos.Film;
using Application.Interfaces;
using Application.Interfaces.Film;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public FilmService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<FilmForDetailedDto> GetOne(int id)
        {
            var film = await _uow.Repository<Film>().FindByIdAsync(id);

            if (film == null)
                return null;

            var filmToReturn = _mapper.Map<FilmForDetailedDto>(film);

            return filmToReturn;
        }

        public async Task<IEnumerable<FilmForListDto>> GetAll()
        {
            var films = await _uow.Repository<Film>().FindAsync();

            var filmsToReturn = _mapper.Map<IEnumerable<FilmForListDto>>(films);

            return filmsToReturn;
        }

        public async Task<FilmForDetailedDto> Create(FilmForCreationDto filmForCreation)
        {
            var film = _mapper.Map<Film>(filmForCreation);

            _uow.Repository<Film>().Add(film);

            await _uow.SaveAsync();

            var filmToReturn = _mapper.Map<FilmForDetailedDto>(film);

            return filmToReturn;
        }
        
    }
}

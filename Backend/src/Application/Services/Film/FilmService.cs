using Application.Dtos.Common;
using Application.Dtos.Film;
using Application.Dtos.Rating;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Film;
using Application.Interfaces.Photo;
using Application.Specifications.Film;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FilmService : IFilmService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhotoService _photoService;
        private readonly IOmdbService _omdbService;
        private readonly IHttpContextAccessor _context;

        public FilmService(IUnitOfWork uow, IMapper mapper, ICurrentUserService currentUserService, IPhotoService photoService, IOmdbService omdbService, IHttpContextAccessor context)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
            _omdbService = omdbService;
            _context = context;
        }

        public async Task<FilmForDetailedDto> GetOne(int id)
        {
            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithAllRelatedDataSpecification(id));

            if (film == null)
                return null;

            var filmToReturn = _mapper.Map<FilmForDetailedDto>(film);

            await _photoService.IncludePhotos(filmToReturn, (int)EntityTypes.Film);

            await _photoService.IncludeMainPhoto(filmToReturn.Participants.Select(x => x.Participant), (int)EntityTypes.Person);

            filmToReturn.ImdbRating = await _omdbService.GetImdbFilmRating(filmToReturn.Title);

            return filmToReturn;
        }

        public async Task<IEnumerable<FilmForListDto>> GetAll(FilmFilterDto filmFilter)
        {
            var films = await _uow.Repository<Film>().FindAsyncWithPagination(new FilmWithGenresFilterPaginatedSpecification(filmFilter));

            _context.HttpContext.Response.AddPagination(films.CurrentPage, films.PageSize, films.TotalCount, films.TotalPages);

            var filmsToReturn = _mapper.Map<IEnumerable<FilmForListDto>>(films);

            await _photoService.IncludeMainPhoto(filmsToReturn, (int)EntityTypes.Film);

            foreach (var filmToReturn in filmsToReturn)
            {
                filmToReturn.ImdbRating = await _omdbService.GetImdbFilmRating(filmToReturn.Title);
            }            

            return filmsToReturn;
        }

        public async Task<FilmForDetailedDto> Create(FilmForCreationDto filmForCreation)
        {
            var film = _mapper.Map<Film>(filmForCreation);

            _uow.Repository<Film>().Add(film);

            await _uow.SaveAsync();

            return await GetOne(film.Id);
        }

        public async Task<FilmForDetailedDto> Update(int id, FilmForUpdateDto filmForUpdate)
        {
            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithParticipantsAndGenresSpecification(id));

            _mapper.Map(filmForUpdate, film);

            await _uow.SaveAsync();

            return await GetOne(film.Id);
        }        

        public async Task Delete(int id)
        {
            var film = await _uow.Repository<Film>().FindByIdAsync(id);

            if (film == null)
                throw new NotFoundException(nameof(Film));

            _uow.Repository<Film>().Remove(film);

            await _uow.SaveAsync();
        }

        public async Task<IEnumerable<RecordNameDto>> GetRecordNames()
        {
            var films = await _uow.Repository<Film>().FindAsync();

            var filmsToReturn = _mapper.Map<IEnumerable<RecordNameDto>>(films);

            return filmsToReturn;
        }

    }
}

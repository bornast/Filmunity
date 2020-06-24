using Application.Dtos.Film;
using Application.Dtos.Rating;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Film;
using Application.Interfaces.Photo;
using Application.Specifications.Film;
using AutoMapper;
using Common.Enums;
using Domain.Entities;
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

        public FilmService(IUnitOfWork uow, IMapper mapper, ICurrentUserService currentUserService, IPhotoService photoService)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
        }

        public async Task<FilmForDetailedDto> GetOne(int id)
        {
            var film = await _uow.Repository<Film>().FindByIdAsync(id);

            if (film == null)
                return null;

            var filmToReturn = _mapper.Map<FilmForDetailedDto>(film);

            filmToReturn.Photos = await _photoService.GetEntityPhotos((int)EntityTypes.Film, id);

            filmToReturn.MainPhoto = filmToReturn.Photos.FirstOrDefault(x => x.IsMain);

            return filmToReturn;
        }

        public async Task<IEnumerable<FilmForListDto>> GetAll(FilmFilterDto filmFilter)
        {
            var films = await _uow.Repository<Film>().FindAsync(new FilmFilterPaginatedSpecification(filmFilter));

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

        public async Task Rate(int id, RatingDto rating)
        {
            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithRatingsSpecification(id));

            var ratingToInsert = new Rating
            {
                UserId = (int)_currentUserService.UserId,
                RatingValue = rating.Rating,
                CreatedAt = DateTime.UtcNow
            };

            film.Ratings.Add(ratingToInsert);

            await _uow.SaveAsync();
        }
        
        public async Task Unrate(int id)
        {
            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithRatingsSpecification(id));

            var ratingToRemove = film.Ratings.FirstOrDefault(x => x.UserId == _currentUserService.UserId);

            film.Ratings.Remove(ratingToRemove);

            await _uow.SaveAsync();
        }
    }
}

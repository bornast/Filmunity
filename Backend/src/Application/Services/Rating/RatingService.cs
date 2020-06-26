using Application.Dtos.Rating;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Photo;
using Application.Interfaces.Rating;
using Application.Specifications.Film;
using AutoMapper;
using Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Rating
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public RatingService(IUnitOfWork uow, ICurrentUserService currentUserService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task Rate(int id, RatingDto rating)
        {
            var film = await _uow.Repository<Film>().FindOneAsync(new FilmWithRatingsSpecification(id));

            var ratingToInsert = new Domain.Entities.Rating
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

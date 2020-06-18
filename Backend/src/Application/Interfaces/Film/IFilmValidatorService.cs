using Application.Dtos.Film;
using Application.Dtos.Rating;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Film
{
    public interface IFilmValidatorService
    {
        public Task ValidateForCreation(FilmForCreationDto filmForCreation);
        Task ValidateForRating(int filmId, RatingDto rating);
        Task ValidateForUnrating(int filmId);
    }
}

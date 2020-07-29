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
        Task ValidateForCreation(FilmForCreationDto filmForCreation);
        Task ValidateForUpdate(int id, FilmForUpdateDto filmForUpdate);
        Task ValidateForDeletion(int id);
    }
}

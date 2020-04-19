using Application.Dtos.Film;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Film
{
    public interface IFilmValidatorService
    {
        public void ValidateForCreation(FilmForCreationDto filmForCreation);
    }
}

using Application.Dtos.Film;
using Application.Interfaces;
using Application.Interfaces.Film;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class FilmValidatorService : BaseValidatorService, IFilmValidatorService
    {
        public FilmValidatorService(
            IServiceProvider serviceProvider,
            IValidatorFactoryService validatorFactoryService
        ) : base(serviceProvider, validatorFactoryService)
        {
        }

        public void ValidateForCreation(FilmForCreationDto filmForCreation)
        {
            Validate(filmForCreation);
        }
    }

}

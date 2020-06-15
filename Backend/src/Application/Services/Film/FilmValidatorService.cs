using Application.Dtos.Film;
using Application.Interfaces;
using Application.Interfaces.Film;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FilmValidatorService : BaseValidatorService, IFilmValidatorService
    {
        private readonly IUnitOfWork _uow;

        public FilmValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow
        ) : base(validatorFactoryService)
        {
            _uow = uow;
        }

        public async Task ValidateForCreation(FilmForCreationDto filmForCreation)
        {
            Validate(filmForCreation);

            var validationErrors = new Dictionary<string, string>();            

            var language = await _uow.Repository<Language>().FindByIdAsync(filmForCreation.LanguageId);            
            AddValidationErrorIfValueIsNull(language, "Language", $"Id {filmForCreation.LanguageId} not found");

            var genres = await _uow.Repository<Genre>().FindAllByIdAsync(filmForCreation.GenreIds);
            AddValidationErrorIfIdDoesntExist(filmForCreation.GenreIds, genres.Select(x => x.Id).ToList(), "Genre", "Id __id__ not found");            

            var participants = await _uow.Repository<Person>().FindAllByIdAsync(filmForCreation.ParticipantsRoles.Select(x => x.ParticipantId).ToList());
            AddValidationErrorIfIdDoesntExist(filmForCreation.ParticipantsRoles.Select(x => x.ParticipantId).ToList(),
                participants.Select(x => x.Id).ToList(), "Participant", "Id __id__ not found");

            var roles = await _uow.Repository<FilmRole>().FindAllByIdAsync(filmForCreation.ParticipantsRoles.Select(x => x.RoleId).ToList());
            AddValidationErrorIfIdDoesntExist(filmForCreation.ParticipantsRoles.Select(x => x.RoleId).ToList(),
                roles.Select(x => x.Id).ToList(), "Role", "Id __id__ not found");

            ThrowValidationErrorsIfNotEmpty();
        }
    }

}

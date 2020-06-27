using Application.Dtos.Person;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Person;
using Common.Enums;
using Common.Libs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Person
{
    public class PersonValidatorService : BaseValidatorService, IPersonValidatorService
    {
        private readonly IUnitOfWork _uow;

        public PersonValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow
        ) : base(validatorFactoryService)
        {
            _uow = uow;
        }

        public void ValidateForSave(PersonForSaveDto personForCreation)
        {
            Validate(personForCreation);

            ValidateGender(personForCreation.GenderId);

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateForSave(int id, PersonForSaveDto personForCreation)
        {
            var person = await _uow.Repository<Domain.Entities.Person>().FindByIdAsync(id);

            AddValidationErrorIfValueIsNull(person, "Person", $"Id {id} not found");

            ValidateForSave(personForCreation);
        }

        private void ValidateGender(int genderId)
        {
            if (!EnumLibrary.GetIntValuesFromEnumType(typeof(Genders)).Contains(genderId))
                AddValidationError("Gender", $"Id {genderId} not found!");
        }

    }
}

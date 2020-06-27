using Application.Dtos.Person;
using Application.Interfaces;
using FluentValidation;
using System;

namespace Application.Validators.Person
{
    public class PersonForSaveDtoValidator : AbstractValidator<PersonForSaveDto>, IObjectValidator<PersonForSaveDtoValidator>
    {
        public PersonForSaveDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            
            RuleFor(x => x.LastName).NotEmpty();
            
            RuleFor(x => x.DateOfBirth).GreaterThan(new DateTime(1900, 1, 1)).LessThanOrEqualTo(DateTime.UtcNow);

            RuleFor(x => x.GenderId).GreaterThan(0);
        }
    }

}

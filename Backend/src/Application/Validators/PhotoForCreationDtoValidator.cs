using Application.Dtos.Photo;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators
{
    public class PhotoForCreationDtoValidator : AbstractValidator<PhotoForCreationDto>, IObjectValidator<PhotoForCreationDtoValidator>
    {
        public PhotoForCreationDtoValidator()
        {
            RuleFor(x => x.File).NotEmpty();

            RuleFor(x => x.EntityTypeId).GreaterThan(0);

            RuleFor(x => x.EntityId).GreaterThan(0);
        }
    }

}

using Application.Dtos.User;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.User
{
    public class UserForUpdateDtoValidator : AbstractValidator<UserForUpdateDto>, IObjectValidator<UserForUpdateDtoValidator>
    {
        public UserForUpdateDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.RoleIds).NotEmpty();
        }
    }
}

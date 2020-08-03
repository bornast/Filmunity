using Application.Dtos.Common;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Common
{
    public class TwitterLoginDtoValidator : AbstractValidator<TwitterLoginDto>, IObjectValidator<TwitterLoginDtoValidator>
    {
        public TwitterLoginDtoValidator()
        {
            RuleFor(x => x.OAuthToken).NotEmpty();
            RuleFor(x => x.OAuthVerifier).NotEmpty();
        }
    }
}

using Application.Dtos.Common;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators.Common
{
    public class FacebookLoginDtoValidator : AbstractValidator<FacebookLoginDto>, IObjectValidator<FacebookLoginDtoValidator>
    {
        public FacebookLoginDtoValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
        }
    }
}

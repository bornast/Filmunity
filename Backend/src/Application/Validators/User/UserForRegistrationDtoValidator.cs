using Application.Dtos.User;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>, IObjectValidator<UserForRegistrationDtoValidator>
    {
        public UserForRegistrationDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}

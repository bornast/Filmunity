using Application.Dtos.User;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>, IObjectValidator<UserForLoginDtoValidator>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(5);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5);
        }
    }
}

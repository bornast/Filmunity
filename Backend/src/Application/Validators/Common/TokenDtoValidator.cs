using Application.Dtos.Common;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators.Common
{
    public class TokenDtoValidator : AbstractValidator<TokenDto>, IObjectValidator<TokenDtoValidator>
    {
        public TokenDtoValidator()
        {
            RuleFor(x => x.Token).NotEmpty();

            RuleFor(x => x.RefreshToken).NotEmpty();            
        }
    }

}

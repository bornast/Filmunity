using Application.Dtos.Rating;
using Application.Interfaces;
using FluentValidation;

namespace Application.Validators.Rating
{
    public class RatingDtoValidator : AbstractValidator<RatingDto>, IObjectValidator<RatingDtoValidator>
    {
        public RatingDtoValidator()
        {
            RuleFor(x => x.Rating).GreaterThanOrEqualTo(1).LessThanOrEqualTo(10);
        }
    }

}

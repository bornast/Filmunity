using Application.Dtos.Review;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Review
{    
    public class ReviewForCreationDtoValidator : AbstractValidator<ReviewForCreationDto>, IObjectValidator<ReviewForCreationDtoValidator>
    {
        public ReviewForCreationDtoValidator()
        {
            RuleFor(x => x.FilmId).GreaterThan(0);
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(400);
        }
    }
}

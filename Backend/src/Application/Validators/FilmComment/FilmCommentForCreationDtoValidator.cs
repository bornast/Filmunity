using Application.Dtos.FilmComment;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.FilmComment
{
    public class FilmCommentForCreationDtoValidator : AbstractValidator<FilmCommentForCreationDto>, IObjectValidator<FilmCommentForCreationDtoValidator>
    {
        public FilmCommentForCreationDtoValidator()
        {
            RuleFor(x => x.FilmId).GreaterThan(0);
            RuleFor(x => x.Comment).NotEmpty().MaximumLength(400);
        }
    }
}

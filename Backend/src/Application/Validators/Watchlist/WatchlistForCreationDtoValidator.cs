using Application.Dtos.Watchlist;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validators.Watchlist
{
    public class WatchlistForCreationDtoValidator : AbstractValidator<WatchlistForCreationDto>, IObjectValidator<WatchlistForCreationDtoValidator>
    {
        public WatchlistForCreationDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();

            RuleFor(x => x.Films).NotEmpty();
        }
    }
}

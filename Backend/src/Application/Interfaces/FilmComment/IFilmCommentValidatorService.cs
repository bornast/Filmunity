using Application.Dtos.FilmComment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.FilmComment
{
    public interface IFilmCommentValidatorService
    {
        Task ValidateForCreation(FilmCommentForCreationDto filmCommentForCreation);
    }
}

using Application.Dtos.FilmComment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.FilmComment
{
    public interface IFilmCommentService
    {
        Task<IEnumerable<FilmCommentForListDto>> GetAll(FilmCommentFilterDto filmCommentFilter);
        Task Create(FilmCommentForCreationDto filmCommentForCreation);
    }
}

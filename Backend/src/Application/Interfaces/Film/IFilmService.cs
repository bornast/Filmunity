using Application.Dtos.Film;
using Application.Dtos.Rating;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Film
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmForListDto>> GetAll(FilmFilterDto filmFilter);
        Task<FilmForDetailedDto> GetOne(int id);
        Task<FilmForDetailedDto> Create(FilmForCreationDto filmForCreation);
        Task<FilmForDetailedDto> Update(int id, FilmForUpdateDto filmForUpdate);
        Task Rate(int id, RatingDto rating);
        Task Unrate(int id);
        Task Delete(int id);
    }
}

using Application.Dtos.Common;
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
        Task Delete(int id);
        Task<IEnumerable<RecordNameDto>> GetRecordNames();
        Task MarkAsWatched(int filmId);
        bool? IsFilmWatched(Domain.Entities.Film film);
    }
}

using Application.Dtos.Film;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Film
{
    public interface IFilmService
    {
        Task<IEnumerable<FilmForListDto>> GetAll();
        Task<FilmForDetailedDto> GetOne(int id);
        Task<FilmForDetailedDto> Create(FilmForCreationDto filmForCreation);
    }
}

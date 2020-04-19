using Application.Dtos.Film;
using System.Threading.Tasks;

namespace Application.Interfaces.Film
{
    public interface IFilmService
    {
        public Task<FilmForDetailedDto> Create(FilmForCreationDto filmForCreation);
    }
}

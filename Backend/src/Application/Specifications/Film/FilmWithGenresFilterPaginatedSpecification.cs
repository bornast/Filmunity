using Application.Dtos.Film;
using System.Linq;

namespace Application.Specifications.Film
{
    public class FilmWithGenresFilterPaginatedSpecification : BaseSpecification<Domain.Entities.Film>
    {
        public FilmWithGenresFilterPaginatedSpecification(FilmFilterDto filmFilter) 
            : base(x => (string.IsNullOrWhiteSpace(filmFilter.Title) || x.Title.ToLower().Contains(filmFilter.Title.ToLower()))
            && (filmFilter.Ids.Count == 0 || filmFilter.Ids.Contains(x.Id))
            && (filmFilter.FilmType == null || x.TypeId == filmFilter.FilmType)
            && (filmFilter.GenreId == null || x.Genres.Select(x => x.GenreId).Contains((int)filmFilter.GenreId)))
        {
            if (filmFilter.OrderByDescending == nameof(Domain.Entities.Film.Rating))
                ApplyOrderByDescending(x => x.Rating);

            if (filmFilter.OrderByDescending == nameof(Domain.Entities.Film.Year))
                ApplyOrderByDescending(x => x.Year);

            ApplyPaging(filmFilter.Skip, filmFilter.Take, filmFilter.PageNumber);

            AddInclude($"{nameof(Domain.Entities.Film.Genres)}.{nameof(Domain.Entities.FilmGenre.Genre)}");
        }
    }
}

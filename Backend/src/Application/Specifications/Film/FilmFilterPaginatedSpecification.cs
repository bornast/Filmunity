using Application.Dtos.Film;

namespace Application.Specifications.Film
{
    public class FilmFilterPaginatedSpecification : BaseSpecification<Domain.Entities.Film>
    {
        public FilmFilterPaginatedSpecification(FilmFilterDto filmFilter) 
            : base(x => (string.IsNullOrWhiteSpace(filmFilter.Title) || x.Title.StartsWith(filmFilter.Title))
            && (filmFilter.Ids.Count == 0 || filmFilter.Ids.Contains(x.Id))
            && (filmFilter.FilmType == null || x.TypeId == filmFilter.FilmType))
        {
            if (filmFilter.OrderBy == nameof(Domain.Entities.Film.Rating))
                ApplyOrderBy(x => x.Rating);

            if (filmFilter.OrderByDescending == nameof(Domain.Entities.Film.Rating))
                ApplyOrderByDescending(x => x.Rating);

            ApplyPaging(filmFilter.Skip, filmFilter.Take);
        }
    }
}

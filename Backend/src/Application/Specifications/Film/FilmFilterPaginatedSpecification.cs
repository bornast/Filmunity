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
            if (filmFilter.OrderBy != null)
                ApplyOrderBy(x => filmFilter.OrderBy);

            if (filmFilter.OrderByDescending != null)
                ApplyOrderByDescending(x => filmFilter.OrderByDescending);

            ApplyPaging(filmFilter.Skip, filmFilter.Take);
        }
    }
}

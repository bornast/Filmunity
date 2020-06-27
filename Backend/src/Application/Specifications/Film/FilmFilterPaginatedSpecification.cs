using Application.Dtos.Film;

namespace Application.Specifications.Film
{
    public class FilmFilterPaginatedSpecification : BaseSpecification<Domain.Entities.Film>
    {
        public FilmFilterPaginatedSpecification(FilmFilterDto filmFilter) 
            : base(x => (string.IsNullOrWhiteSpace(filmFilter.Title) || x.Title.StartsWith(filmFilter.Title))
            && (filmFilter.Ids.Count == 0 || filmFilter.Ids.Contains(x.Id)))
        {
            ApplyPaging(filmFilter.Skip, filmFilter.Take);
        }
    }
}

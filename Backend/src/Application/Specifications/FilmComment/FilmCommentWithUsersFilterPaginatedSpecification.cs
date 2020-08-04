using Application.Dtos.FilmComment;

namespace Application.Specifications.FilmComment
{
    public class FilmCommentWithUsersFilterPaginatedSpecification : BaseSpecification<Domain.Entities.FilmComment>
    {
        public FilmCommentWithUsersFilterPaginatedSpecification(FilmCommentFilterDto filmCommentFilter)
            : base(x => filmCommentFilter.FilmId == null || x.FilmId == filmCommentFilter.FilmId)
        {
            if (filmCommentFilter.OrderByDescending == nameof(Domain.Entities.FilmComment.CreatedAt))
                ApplyOrderByDescending(x => x.CreatedAt);

            ApplyPaging(filmCommentFilter.Skip, filmCommentFilter.Take, filmCommentFilter.PageNumber);

            AddInclude($"{nameof(Domain.Entities.FilmComment.User)}");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.FilmComment
{
    public class FilmCommentFilterSpecification : BaseSpecification<Domain.Entities.FilmComment>
    {
        public FilmCommentFilterSpecification(int filmId, int userId, string comment)
            : base(x => x.FilmId == filmId && x.UserId == userId && x.Comment == comment)
        {

        }
    }
}

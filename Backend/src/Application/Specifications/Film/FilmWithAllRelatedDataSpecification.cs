using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Film
{
    public class FilmWithAllRelatedDataSpecification : BaseSpecification<Domain.Entities.Film>
    {
        public FilmWithAllRelatedDataSpecification(int id)
            : base(x => x.Id == id)
        {            
            AddInclude($"{nameof(Domain.Entities.Film.Type)}");
            AddInclude($"{nameof(Domain.Entities.Film.Country)}");
            AddInclude($"{nameof(Domain.Entities.Film.Language)}");
            AddInclude($"{nameof(Domain.Entities.Film.WatchedByUsers)}");
            AddInclude($"{nameof(Domain.Entities.Film.Genres)}.{nameof(Domain.Entities.FilmGenre.Genre)}");
            AddInclude($"{nameof(Domain.Entities.Film.Participants)}.{nameof(Domain.Entities.FilmParticipant.Person)}");
            AddInclude($"{nameof(Domain.Entities.Film.Participants)}.{nameof(Domain.Entities.FilmParticipant.FilmRole)}");
        }
    }
}

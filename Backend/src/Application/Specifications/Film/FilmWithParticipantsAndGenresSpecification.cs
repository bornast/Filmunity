namespace Application.Specifications.Film
{
    public class FilmWithParticipantsAndGenresSpecification : BaseSpecification<Domain.Entities.Film>
    {
        public FilmWithParticipantsAndGenresSpecification(int id)
            : base(x => x.Id == id)
        {            
            AddInclude($"{nameof(Domain.Entities.Film.Genres)}.{nameof(Domain.Entities.FilmGenre.Genre)}");
            AddInclude($"{nameof(Domain.Entities.Film.Pariticpants)}.{nameof(Domain.Entities.FilmParticipant.Person)}");
            AddInclude($"{nameof(Domain.Entities.Film.Pariticpants)}.{nameof(Domain.Entities.FilmParticipant.FilmRole)}");
        }
    }
}
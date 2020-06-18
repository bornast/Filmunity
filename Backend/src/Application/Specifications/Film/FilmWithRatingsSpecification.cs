using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Film
{
    public class FilmWithRatingsSpecification : BaseSpecification<Domain.Entities.Film>
    {
        public FilmWithRatingsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude($"{nameof(Domain.Entities.Film.Ratings)}");
        }
    }    
}

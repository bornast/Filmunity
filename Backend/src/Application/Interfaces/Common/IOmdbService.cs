using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface IOmdbService
    {
        Task<float> GetImdbFilmRating(string filmTitle);
    }
}

using Application.Dtos.Common;
using Application.Interfaces.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Genre
{
    public interface IGenreService
    {
        Task<IEnumerable<RecordNameDto>> GetRecordNames();
    }
}

using Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.FilmRole
{
    public interface IFilmRoleService
    {
        Task<IEnumerable<RecordNameDto>> GetRecordNames();
    }
}

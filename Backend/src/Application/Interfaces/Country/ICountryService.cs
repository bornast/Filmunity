using Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Country
{
    public interface ICountryService
    {
        Task<IEnumerable<RecordNameDto>> GetRecordNames();
    }
}

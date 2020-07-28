using Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Language
{
    public interface ILanguageService
    {
        Task<IEnumerable<RecordNameDto>> GetRecordNames();
    }
}

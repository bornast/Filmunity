using Application.Dtos.Common;
using Application.Dtos.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Person
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonForListDto>> GetAll();
        Task<PersonForDetailedDto> GetOne(int id);
        Task<PersonForDetailedDto> Create(PersonForSaveDto personForCreation);
        Task<PersonForDetailedDto> Update(int id, PersonForSaveDto personForUpdate);
        Task Delete(int id);
        Task<IEnumerable<RecordNameDto>> GetRecordNames();
    }
}

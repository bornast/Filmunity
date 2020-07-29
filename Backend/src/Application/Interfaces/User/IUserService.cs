using Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserForListDto>> GetAll(UserFilterDto userFilter);
        Task<UserForDetailedDto> GetOne(int id);
        Task<UserForDetailedDto> Update(int id, UserForUpdateDto userForUpdate);
        Task Delete(int id);
    }
}

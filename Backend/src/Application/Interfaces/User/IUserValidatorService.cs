using Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.User
{
    public interface IUserValidatorService
    {
        Task ValidateForUpdate(int id, UserForUpdateDto userForUpdate);
    }
}

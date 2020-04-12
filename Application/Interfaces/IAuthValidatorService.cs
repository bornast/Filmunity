using Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthValidatorService
    {
        public Task ValidateForRegistration(UserForRegistrationDto userForRegistration);
        public void ValidateForLogin(UserForLoginDto userForLogin);
    }
}

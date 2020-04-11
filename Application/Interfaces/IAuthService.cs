using Application.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        public void Login(UserForLoginDto userForLogin);
        public void Register(UserForRegistrationDto userForRegistration);
    }
}

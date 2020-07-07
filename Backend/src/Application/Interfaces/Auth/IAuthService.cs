using Application.Dtos.Common;
using Application.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        public Task<TokenDto> Login(UserForLoginDto userForLogin);
        public Task Register(UserForRegistrationDto userForRegistration);
        Task<TokenDto> RefreshToken(TokenDto tokenForRefresh);
    }
}

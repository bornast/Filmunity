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
        Task<TokenDto> Login(UserForLoginDto userForLogin);
        Task Register(UserForRegistrationDto userForRegistration);
        Task<TokenDto> RefreshToken(TokenDto tokenForRefresh);
        // TODO: rename all async methods with suffix Async: eg. LoginWithFacebookAsync
        Task<TokenDto> LoginWithFacebook(FacebookLoginDto facebookLogin);
    }
}

using Application.Dtos.Common;
using Application.Dtos.User;
using Application.Models;
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
        Task<TokenDto> LoginWithFacebook(FacebookLoginDto facebookLogin);
        TwitterTokenResponse GetTwitterRequestToken();
        Task<TokenDto> LoginWithTwitter(TwitterLoginDto twitterLogin);
    }
}

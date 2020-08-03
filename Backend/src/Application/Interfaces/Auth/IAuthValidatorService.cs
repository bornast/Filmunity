using Application.Dtos.Common;
using Application.Dtos.User;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthValidatorService
    {
        Task ValidateForRegistration(UserForRegistrationDto userForRegistration);
        void ValidateForLogin(UserForLoginDto userForLogin);
        Task ValidateBeforeTokenRefresh(TokenDto tokenForRefresh);
        Task ValidateForLoginWithFacebook(FacebookLoginDto facebookLogin);
        void ValidateForLoginWithTwitter(TwitterLoginDto twitterLogin);
    }
}

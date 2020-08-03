using Application.Dtos.Common;
using Application.Interfaces.Common;
using Application.Models;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TweetSharp;

namespace Infrastructure.Services
{
    public class TwitterService : Application.Interfaces.Common.ITwitterService
    {
        private readonly TwitterSettings _twitterSettings;

        public TwitterService(TwitterSettings twitterSettings)
        {
            _twitterSettings = twitterSettings;
        }

        public TwitterTokenResponse GetRequestToken()
        {
            TweetSharp.TwitterService service = new TweetSharp.TwitterService(_twitterSettings.AppId, _twitterSettings.AppSecret);
            
            OAuthRequestToken requestToken = service.GetRequestToken(_twitterSettings.CallbackUrl);

            var twitterResponse = new TwitterTokenResponse
            {
                OAuthToken = requestToken.Token,
                OAuthTokenSecret = requestToken.TokenSecret,
                OAuthCallbackConfirmed = requestToken.OAuthCallbackConfirmed
            };

            return twitterResponse;
        }

        public Application.Models.TwitterUser GetUserInfo(TwitterLoginDto twitterLogin)
        {            
            TweetSharp.TwitterService service = new TweetSharp.TwitterService(_twitterSettings.AppId, _twitterSettings.AppSecret);

            // this is only temporary for testing purposes
            service.AuthenticateWith(_twitterSettings.AccessToken, _twitterSettings.AccessTokenSecret);

            // TODO: when we go in to production
            //var requestToken = new OAuthRequestToken
            //{
            //    Token = twitterLogin.OAuthToken
            //};
            //OAuthAccessToken accessToken = service.GetAccessToken(requestToken, twitterLogin.OAuthVerifier);
            //service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);            

            var user = service.VerifyCredentials(new VerifyCredentialsOptions());

            return new Application.Models.TwitterUser
            {
                Name = user.Name,
                ScreenName = user.ScreenName
            };            
        }
    }

}

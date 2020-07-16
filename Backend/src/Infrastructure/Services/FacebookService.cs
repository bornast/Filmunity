using Application.Interfaces.Common;
using Application.Models;
using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FacebookService : IFacebookService
    {
        private const string _tokenValidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string _userInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,email&access_token={0}";
        private readonly FacebookSettings _facebookSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        // TODO: extract httpclientfactory into service so it can be unit tested
        public FacebookService(FacebookSettings facebookSettings, IHttpClientFactory httpClientFactory)
        {
            _facebookSettings = facebookSettings;
            _httpClientFactory = httpClientFactory;
        }

        // facebooktokenvalidationresult should be a model in infrastructure
        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            // TODO: this method should return bool value
            var formattedUrl = string.Format(_tokenValidationUrl, accessToken, _facebookSettings.AppId, _facebookSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }

        public async Task<FacebookUser> GetUserInfoAsync(string accessToken)
        {
            var formattedUrl = string.Format(_userInfoUrl, accessToken);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookUser>(responseAsString);
        }        
    }
}

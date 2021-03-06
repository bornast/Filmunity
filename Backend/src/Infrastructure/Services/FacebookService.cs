﻿using Application.Interfaces.Common;
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
        private readonly IHttpService _httpService;

        public FacebookService(FacebookSettings facebookSettings, IHttpService httpService)
        {
            _facebookSettings = facebookSettings;
            _httpService = httpService;
        }

        public async Task<bool> ValidateAccessTokenAsync(string accessToken)
        {
            try
            {
                var formattedUrl = string.Format(_tokenValidationUrl, accessToken, _facebookSettings.AppId, _facebookSettings.AppSecret);

                var result = await _httpService.GetAsync(formattedUrl);

                result.EnsureSuccessStatusCode();

                var responseAsString = await result.Content.ReadAsStringAsync();

                var validatedTokenResult = JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);

                return validatedTokenResult.Data.IsValid;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<FacebookUser> GetUserInfoAsync(string accessToken)
        {
            var formattedUrl = string.Format(_userInfoUrl, accessToken);

            var result = await _httpService.GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookUser>(responseAsString);
        }        
    }
}

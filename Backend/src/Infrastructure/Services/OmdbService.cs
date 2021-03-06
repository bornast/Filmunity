﻿using Application.Interfaces.Common;
using Infrastructure.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OmdbService : IOmdbService
    {
        private const string _filmInfoUrl = "http://www.omdbapi.com/?t={0}&apikey={1}";
        private readonly OmdbSettings _omdbSettings;
        private readonly IHttpService _httpService;

        public OmdbService(OmdbSettings omdbSettings, IHttpService httpService)
        {
            _omdbSettings = omdbSettings;
            _httpService = httpService;
        }

        public async Task<float> GetImdbFilmRating(string filmTitle)
        {
            try
            {
                var formattedUrl = string.Format(_filmInfoUrl, filmTitle, _omdbSettings.ApiKey);

                var result = await _httpService.GetAsync(formattedUrl);

                result.EnsureSuccessStatusCode();

                var responseAsString = await result.Content.ReadAsStringAsync();

                var omdbResponse = JsonConvert.DeserializeObject<OmdbResponse>(responseAsString);

                return float.Parse(omdbResponse.ImdbRating, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception)
            {
                return 0.0f;
            }
        }
    }
}

using Application.Interfaces.Common;
using FluentAssertions;
using Infrastructure.Models;
using Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Infrastructure.Services
{
    [TestFixture]
    public class OmdbServiceTests
    {
        #region fields

        private Mock<IHttpService> _httpService;
        private string _movieDoesntExistTitle;
        private string _movieExistsTitle;
        private OmdbService _service;
        private OmdbSettings _omdbSettings;
        private HttpResponseMessage _invalidHttpResponseMessage;
        private HttpResponseMessage _validHttpResponseMessage;

        #endregion

        #region setup

        [SetUp]
        public void SetUp()
        {
            InitializeMocks();
            InitializeObjects();
            InitializeMockSetup();
        }

        #endregion

        #region tests

        [Test]
        public async Task GetImdbFilmRating_MovieDoesntExist_Return0()
        {
            var result = await _service.GetImdbFilmRating(_movieDoesntExistTitle);

            result.Should().Be(0.0f);
        }

        [Test]
        public async Task GetImdbFilmRating_MovieExists_ReturnFloatGreatherThan0()
        {
            var result = await _service.GetImdbFilmRating(_movieExistsTitle);

            result.Should().BeGreaterThan(0.0f);
        }

        #endregion

        #region private methods

        private void InitializeMocks()
        {
            _httpService = new Mock<IHttpService>();
        }

        private void InitializeObjects()
        {
            _omdbSettings = new OmdbSettings
            {
                ApiKey = "apiKey"
            };

            _service = new OmdbService(_omdbSettings, _httpService.Object);

            _movieDoesntExistTitle = "movie-doesnt-exist";

            _movieExistsTitle = "movie-exists";

            _invalidHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            _validHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"Title\":\"Desperados\",\"Year\":\"2007–\",\"Rated\":\"N/A\",\"Released\":\"31 Jan 2007\",\"Runtime\":\"30 min\",\"Genre\":\"Drama, Family\",\"Director\":\"N/A\",\"Writer\":\"Paul Smith\",\"Actors\":\"Ade Adepitan, Cole Edwards, David Proud, Duane Henry\",\"Plot\":\"Desperados is a children's drama about a wheelchair basketball team. Following an accident which leaves him disabled, Charlie finds new meaning to his life when he joins the Desperados team.\",\"Language\":\"English\",\"Country\":\"UK\",\"Awards\":\"1 win & 1 nomination.\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BYWJhZmFhZTktNTg2Zi00YzU3LTg1YjgtYmM3YWVlMmE3NDNlXkEyXkFqcGdeQXVyMzUyOTk3NjQ@._V1_SX300.jpg\",\"Ratings\":[{\"Source\":\"Internet Movie Database\",\"Value\":\"8.0/10\"}],\"Metascore\":\"N/A\",\"imdbRating\":\"8.0\",\"imdbVotes\":\"37\",\"imdbID\":\"tt1039167\",\"Type\":\"series\",\"totalSeasons\":\"1\",\"Response\":\"True\"}")
            };
        }

        private void InitializeMockSetup()
        {
            _httpService.Setup(x => x.GetAsync(It.Is<string>(x => x.Contains(_movieDoesntExistTitle)))).Returns(() => Task.FromResult(_invalidHttpResponseMessage));
            _httpService.Setup(x => x.GetAsync(It.Is<string>(x => x.Contains(_movieExistsTitle)))).Returns(() => Task.FromResult(_validHttpResponseMessage));
        }

        #endregion
    }
}

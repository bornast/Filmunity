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
    public class FacebookServiceTests
    {
        #region fields

        private Mock<IHttpService> _httpService;
        private FacebookService _service;
        private FacebookSettings _facebookSettings;
        private string _invalidAccessToken;
        private string _validAccessToken;
        private string _validUserInfoAccessToken;
        private HttpResponseMessage _invalidHttpResponseMessage;
        private HttpResponseMessage _validHttpResponseMessage;
        private HttpResponseMessage _validUserInfoHttpResponseMessage;

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
        public async Task ValidateAccessTokenAsync_InvalidToken_ReturnFalse()
        {
            var result = await _service.ValidateAccessTokenAsync(_invalidAccessToken);

            result.Should().BeFalse();
        }

        [Test]
        public async Task ValidateAccessTokenAsync_ValidToken_ReturnTrue()
        {
            var result = await _service.ValidateAccessTokenAsync(_validAccessToken);

            result.Should().BeTrue();
        }

        [Test]
        public async Task GetUserInfoAsync_WhenCalled_ReturnsFacebookUser()
        {
            var result = await _service.GetUserInfoAsync(_validUserInfoAccessToken);

            result.Should().NotBeNull();
            result.FirstName.Should().Be("Filmunity");
        }
        
        #endregion

        #region private methods

        private void InitializeMocks()
        {
            _httpService = new Mock<IHttpService>();
        }

        private void InitializeObjects()
        {
            _facebookSettings = new FacebookSettings
            {
                AppId = "appId",
                AppSecret = "appSecret"
            };

            _service = new FacebookService(_facebookSettings, _httpService.Object);

            _invalidAccessToken = "Invalid-access-token";

            _validAccessToken = "Valid-access-token";

            _validUserInfoAccessToken = "Valid-user-info-access-token";

            _invalidHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            _validHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"data\":{\"app_id\":\"123\",\"type\":\"USER\",\"application\":\"filmunity\",\"data_access_expires_at\":123,\"expires_at\":123,\"is_valid\":true,\"scopes\":[\"email\",\"public_profile\"],\"user_id\":\"123\"}}")
            };

            _validUserInfoHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"first_name\":\"Filmunity\",\"last_name\":\"Filmunity\",\"email\":\"email\\u0040email.com\",\"id\":\"123\"}")
            };            
        }

        private void InitializeMockSetup()
        {
            _httpService.Setup(x => x.GetAsync(It.Is<string>(x => x.Contains(_invalidAccessToken)))).Returns(() => Task.FromResult(_invalidHttpResponseMessage));
            _httpService.Setup(x => x.GetAsync(It.Is<string>(x => x.Contains(_validAccessToken)))).Returns(() => Task.FromResult(_validHttpResponseMessage));
            _httpService.Setup(x => x.GetAsync(It.Is<string>(x => x.Contains(_validUserInfoAccessToken)))).Returns(() => Task.FromResult(_validUserInfoHttpResponseMessage));
        }

        #endregion
    }
}

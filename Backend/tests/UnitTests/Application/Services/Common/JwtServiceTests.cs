using Application.Interfaces.Common;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UnitTests.Application.Services.Common
{
    [TestFixture]
    public class JwtServiceTests
    {
        private JwtService _service;

        [SetUp]
        public void Setup()
        {
            var configuration = new Mock<IConfigurationRoot>();
            configuration.Setup(x => x.GetSection(It.IsAny<string>()).Value).Returns("super secret key");

            var refreshTokenService = new Mock<IRefreshTokenService>();
            refreshTokenService.Setup(x => x.CreateRefreshToken(It.IsAny<string>(), It.IsAny<User>(), It.IsAny<bool>())).Returns(() => Task.FromResult(new RefreshToken { Token = "token" }));

            _service = new JwtService(configuration.Object, new TokenValidationParameters(), refreshTokenService.Object);
        }

        [Test]
        public void GenerateJwtToken_UserIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _service.GenerateJwtToken(null), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public async Task GenerateJwtToken_WhenCalled_ReturnToken()
        {
            var user = new User
            { 
                Id = 1,
                Username = "username"
            };

            var result = await _service.GenerateJwtToken(user);

            result.Should().NotBeNull();

            result.Token.Should().NotBeNull();

            result.RefreshToken.Should().Be("token");
        }

    }
}

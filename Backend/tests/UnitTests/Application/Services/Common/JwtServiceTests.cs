using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;

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
            _service = new JwtService(configuration.Object);
        }

        [Test]
        public void GenerateJwtToken_UserIsNull_ThrowArgumentNullException()
        {
            Assert.That(() => _service.GenerateJwtToken(null), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GenerateJwtToken_WhenCalled_ReturnToken()
        {
            var user = new User
            { 
                Id = 1,
                Username = "username"
            };

            var result = _service.GenerateJwtToken(user);

            Assert.IsTrue(result.Length > 0);
        }

    }
}

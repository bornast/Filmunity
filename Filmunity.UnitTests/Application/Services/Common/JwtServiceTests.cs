using Application.Services;
using Domain.Entities;
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
            _service = new JwtService();
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

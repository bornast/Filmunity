using Application.Services;
using NUnit.Framework;

namespace UnitTests.Application.Services.Common
{
    [TestFixture]
    public class HashServiceTests
    {
        private HashService _service;
        [SetUp]
        public void SetUp()
        {
            _service = new HashService();
        }

        [TestCase("password1")]
        [TestCase("password2")]
        public void CreatePasswordHash_WhenCalled_ReturnPasswordObject(string passord)
        {
            var result = _service.CreatePasswordHash(passord);
            Assert.Multiple(() =>
            {
                Assert.That(result.PasswordHash, Is.Not.Null);
                Assert.That(result.PasswordSalt, Is.Not.Null);
            });
            
        }

    }
}

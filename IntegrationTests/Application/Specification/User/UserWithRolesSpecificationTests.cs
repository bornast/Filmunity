using Application.Specifications;
using Common.Enums;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static IntegrationTests.Testing;

namespace IntegrationTests.Application.Specification
{
    [TestFixture]
    public class UserWithRolesSpecificationTests : TestBase
    {        
        [TestCase("username-for-testing-purposes1")]
        [TestCase("username-for-testing-purposes2")]
        public async Task ShouldReturnOneUser(string username)
        {
            // TODO: add roles to a user aswell
            // Arrange
            await AddAsync(new User
            {
                Username = username,
                Email = $"{username}@gmail.com",
                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Status = Status.WaitingActivation
                
            });

            // Act
            var result = FindOne(new UserWithRolesSpecification(username));

            // Assert
            result.Should().NotBeNull();
            result.Username.Should().Be(username);
        }


        [TestCase("username-for-testing-purposes1")]
        [TestCase("username-for-testing-purposes2")]
        public async Task ShouldReturnNoUsers(string username)
        {
            // Arrange            
            await AddAsync(new User
            {
                Username = $"{username}-modify",
                Email = $"{username}@gmail.com",
                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
                Status = Status.WaitingActivation
            });

            // Act
            var result = FindOne(new UserWithRolesSpecification(username));

            // Assert
            result.Should().BeNull();
        }

    }
}





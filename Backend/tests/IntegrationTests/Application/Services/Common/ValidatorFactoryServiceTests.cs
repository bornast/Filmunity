using NUnit.Framework;
using System;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces.Common;
using Application.Services;
using Application.Dtos.User;
using FluentAssertions;
using Moq;
using Application.Interfaces;

namespace IntegrationTests.Application.Services.Common
{
    using static Testing;

    [TestFixture]
    public class ValidatorFactoryServiceTests
    {
        [Test]
        public void ShouldReturnValidator()
        {
            using var scope = _scopeFactory.CreateScope();

            var serviceProvider = scope.ServiceProvider.GetService<IServiceProvider>();

            var typeService = scope.ServiceProvider.GetService<ITypeService>();

            var validatorFactoryService = new ValidatorFactoryService(serviceProvider, typeService);

            var result = validatorFactoryService.GetValidator(new UserForLoginDto());

            result.Should().NotBeNull();
        }

        [Test]
        public void ShouldThrowNullException()
        {
            using var scope = _scopeFactory.CreateScope();

            var serviceProvider = scope.ServiceProvider.GetService<IServiceProvider>();

            var typeService = scope.ServiceProvider.GetService<ITypeService>();

            var validatorFactoryService = new ValidatorFactoryService(serviceProvider, typeService);

            FluentActions.Invoking(() =>
                validatorFactoryService.GetValidator(new Mock<IObjectToValidate>().Object))
                .Should().Throw<ArgumentNullException>();
        }

    }

}

using Application.Interfaces;
using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class ValidatorFactoryService : IValidatorFactoryService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidatorFactoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IValidator GetValidator(IObjectToValidate objectToValidate)
        {
            var validatorName = $"{objectToValidate.GetType().Name}Validator";

            var validatorType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(t => t.Name == validatorName);
            Guard.Against.Null(validatorType, nameof(validatorType));

            var validatorInterfaceType = validatorType.GetInterfaces()
                .FirstOrDefault(x => x.FullName.Contains("ObjectValidator"));
            Guard.Against.Null(validatorInterfaceType, nameof(validatorInterfaceType));

            var validatorService = (IValidator)_serviceProvider
                .GetRequiredService(validatorInterfaceType);
            Guard.Against.Null(validatorService, nameof(validatorService));

            return validatorService;
        }
    }
}

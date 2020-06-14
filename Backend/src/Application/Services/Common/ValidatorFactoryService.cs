using Application.Interfaces;
using Application.Interfaces.Common;
using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Application.Services
{
    public class ValidatorFactoryService : IValidatorFactoryService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ITypeService _typeService;

        public ValidatorFactoryService(IServiceProvider serviceProvider, ITypeService typeService)
        {
            _serviceProvider = serviceProvider;
            _typeService = typeService;
        }

        public IValidator GetValidator(IObjectToValidate objectToValidate)
        {
            Guard.Against.Null(objectToValidate, nameof(objectToValidate));

            var validatorName = $"{objectToValidate.GetType().Name}Validator";
            
            var validatorType = _typeService.GetClassTypeByName(validatorName);

            var validatorInterfaceType = _typeService.GetInterfaceTypeFromClassByName("ObjectValidator", validatorType);

            var validatorService = (IValidator)_serviceProvider
                .GetRequiredService(validatorInterfaceType);

            return validatorService;
        }
    }
}

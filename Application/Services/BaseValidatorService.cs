using Application.Helpers;
using Application.Interfaces;
using Ardalis.GuardClauses;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using ValidationException = Common.Exceptions.ValidationException;

namespace Application.Services
{
    public abstract class BaseValidatorService
    {
        private readonly IServiceProvider _serviceProvider;

        public BaseValidatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected static void ThrowValidationError(string propertyName, string errorMsg)
        {
            var validationErrors = new ValidationErrors();
            validationErrors.AddError(propertyName, errorMsg);
            throw new ValidationException(validationErrors.Errors);
        }

        protected void Validate(IObjectToValidate objectToValidate)
        {
            var validator = GetValidatorFactory($"{objectToValidate.GetType().Name}Validator");

            var result = validator.Validate(objectToValidate);

            if (result.Errors.Count > 0)
            {
                var validationErrors = GetValidationErrors(result.Errors);
                throw new ValidationException(validationErrors);
            }
        }        

        private static Dictionary<string, List<string>> GetValidationErrors(IList<ValidationFailure> errors)
        {
            var validationErrors = new ValidationErrors();
            foreach (var error in errors)
            {
                validationErrors.AddError(error.PropertyName, error.ErrorMessage);
            }
            return validationErrors.Errors;
        }

        private IValidator GetValidatorFactory(string validatorName)
        {
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

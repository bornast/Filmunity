using Application.Models;
using Application.Interfaces;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using ValidationException = Common.Exceptions.ValidationException;

namespace Application.Services
{
    public abstract class BaseValidatorService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IValidatorFactoryService _validatorFactoryService;

        public BaseValidatorService(IServiceProvider serviceProvider, IValidatorFactoryService validatorFactoryService)
        {
            _serviceProvider = serviceProvider;
            _validatorFactoryService = validatorFactoryService;
        }

        protected void Validate(IObjectToValidate objectToValidate)
        {
            var validator = _validatorFactoryService.GetValidator(objectToValidate);

            var result = validator.Validate(objectToValidate);

            if (result.Errors.Count > 0)
            {
                var validationErrors = GetValidationErrors(result.Errors);
                throw new ValidationException(validationErrors);
            }
        }

        protected static void ThrowValidationError(string propertyName, string errorMsg)
        {
            var validationErrors = new ValidationErrors();

            validationErrors.AddError(propertyName, errorMsg);

            throw new ValidationException(validationErrors.Errors);
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

    }
}

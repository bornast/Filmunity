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
        private readonly IValidatorFactoryService _validatorFactoryService;
        private readonly Dictionary<string, string> _validationErrors;

        public BaseValidatorService(IValidatorFactoryService validatorFactoryService)
        {
            _validatorFactoryService = validatorFactoryService;
            _validationErrors = new Dictionary<string, string>();
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

        private static Dictionary<string, List<string>> GetValidationErrors(IList<ValidationFailure> errors)
        {
            var validationErrors = new ValidationErrors();

            foreach (var error in errors)
            {
                validationErrors.AddError(error.PropertyName, error.ErrorMessage);
            }

            return validationErrors.Errors;
        }

        protected void ThrowValidationErrorsIfNotEmpty()
        {
            if (_validationErrors.Count == 0)
                return;

            var validationErrors = new ValidationErrors();

            foreach (KeyValuePair<string, string> error in _validationErrors)
            {
                validationErrors.AddError(error.Key, error.Value);
            }

            throw new ValidationException(validationErrors.Errors);
        }

        protected void AddValidationErrorIfValueIsNull(object value, string key, string msg)
        {
            if (value == null)
                _validationErrors.Add(key, msg);
        }

        protected void AddValidationErrorIfIdDoesntExist(List<int> idsFromRequest, List<int> idsFromDb, string key, string msg)
        {
            foreach (var id in idsFromRequest)
            {
                if (!idsFromDb.Contains(id))
                    _validationErrors.Add(key, msg.Replace("__id__", id.ToString()));
            }
        }
    }
}

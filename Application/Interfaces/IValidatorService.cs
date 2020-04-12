using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IValidatorService
    {
        void Validate(IObjectToValidate objectToValidate);
        void ThrowValidationError(string propertyName, string errorMsg);
    }
}

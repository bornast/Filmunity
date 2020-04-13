using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IValidatorFactoryService
    {
        IValidator GetValidator(IObjectToValidate objectToValidate);
    }
}

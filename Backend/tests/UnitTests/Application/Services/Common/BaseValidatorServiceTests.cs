using Application.Interfaces;
using Application.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Common.Libs;

namespace Filmunity.UnitTests.Application.Services
{
    [TestFixture]
    public class BaseValidatorServiceTests
    {
        private Mock<IServiceProvider> _serviceProvider;
        private Mock<IValidatorFactoryService> _validatorFactoryService;
        private Mock<IObjectToValidate> _objectToValidate;
        private Mock<IValidator> _validatorObject;
        private Mock<ValidationResult> _validationResults;
        private List<ValidationFailure> _emptyValidationFailures;
        private List<ValidationFailure> _notEmptyValidationFailures;
        private ValidationMock _service;

        [SetUp]
        public void SetUp()
        {
            InitializeMocks();
            InitializeObjects();
            InitializeMockSetup();
        }

        [Test]
        public void Validate_ValidationFailed_ThrowValidationException()
        {
            ReflectionLibrary.SetValueToReadonlyProperty(_validationResults.Object, typeof(ValidationResult), 
                "Errors", _notEmptyValidationFailures);
            
            Assert.That(() => _service.Validate(_objectToValidate.Object), Throws.Exception.TypeOf<Common.Exceptions.ValidationException>());
        }

        [Test]
        public void Validate_NoValidationErrors_DontThrowException()
        {
            ReflectionLibrary.SetValueToReadonlyProperty(_validationResults.Object, typeof(ValidationResult),
                "Errors", _emptyValidationFailures);            

            Assert.DoesNotThrow(() => _service.Validate(_objectToValidate.Object));
        }

        private void InitializeMocks()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _validatorFactoryService = new Mock<IValidatorFactoryService>();
            _objectToValidate = new Mock<IObjectToValidate>();
            _validatorObject = new Mock<IValidator>();
            _validationResults = new Mock<ValidationResult>();
        }

        private void InitializeObjects()
        {
            _notEmptyValidationFailures = new List<ValidationFailure> { new ValidationFailure("a", "a") };
            _emptyValidationFailures = new List<ValidationFailure>();
            _service = new ValidationMock(_serviceProvider.Object, _validatorFactoryService.Object);
        }

        private void InitializeMockSetup()
        {
            _validatorObject.Setup(x => x.Validate(_objectToValidate.Object)).Returns(_validationResults.Object);
            _validatorFactoryService.Setup(x => x.GetValidator(_objectToValidate.Object)).Returns(_validatorObject.Object);
        }


    }

    public class ValidationMock : BaseValidatorService
    {
        public ValidationMock(
            IServiceProvider serviceProvider, 
            IValidatorFactoryService validatorFactoryService
        ) : base(serviceProvider, validatorFactoryService) { }

        public new void Validate(IObjectToValidate objectToValidate)
        {
            base.Validate(objectToValidate);
        }
    }

}

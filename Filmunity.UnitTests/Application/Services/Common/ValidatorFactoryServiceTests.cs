using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Services;
using Moq;
using NUnit.Framework;
using System;

namespace UnitTests.Application.Services.Common
{
    [TestFixture]
    public class ValidatorFactoryServiceTests
    {
        private Mock<IServiceProvider> _serviceProvider;
        private Mock<ITypeService> _typeService;
        private ValidatorFactoryService _service;
        private IObjectToValidate _objectToValidate;
        private Type _validatorType;

        [SetUp]
        public void SetUp()
        {
            InitializeMocks();
            InitializeObjects();
        }

        [Test]
        public void GetValidator_ObjectToValidateIsNull_ThrowArgumentNullException()
        {            
            Assert.That(() => _service.GetValidator(null), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetValidator_ObjecToValidateDoesntHaveAValidator_ThrowArgumentNullException()
        {            
            _typeService.Setup(x => x.GetClassTypeByName("ObjectToValidateValidator")).Throws<ArgumentNullException>();
            Assert.That(() => _service.GetValidator(_objectToValidate), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void GetValidator_ObjectValidatorDoesntImplementInterface_ThrowArgumentNullException()
        {
            _typeService.Setup(x => x.GetClassTypeByName("ObjectToValidateValidator")).Returns(_validatorType);
            _typeService.Setup(x => x.GetInterfaceTypeFromClassByName("ObjectToValidateValidator", _validatorType)).Throws<ArgumentNullException>();
            Assert.That(() => _service.GetValidator(_objectToValidate), Throws.Exception.TypeOf<ArgumentNullException>());
        }

        // i cannot test the GetRequiredService method to see if validator is being return since GetRequiredService method cannot be mocked        

        private void InitializeMocks()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _typeService = new Mock<ITypeService>();
        }

        private void InitializeObjects()
        {
            _objectToValidate = new ObjectToValidate();
            _service = new ValidatorFactoryService(_serviceProvider.Object, _typeService.Object);
            _validatorType = new Mock<Type>().Object;
        }

    }

    public class ObjectToValidate : IObjectToValidate
    {

    }

}

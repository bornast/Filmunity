using Application.Dtos.User;
using Application.Interfaces;
using Application.Specifications;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthValidatorService : BaseValidatorService, IAuthValidatorService
    {
        private readonly IUnitOfWork _uow;

        public AuthValidatorService(
            IUnitOfWork uow, 
            IValidatorFactoryService validatorFactoryService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
        }
        
        public void ValidateForLogin(UserForLoginDto userForLogin)
        {
            Validate(userForLogin);
        }
        
        public async Task ValidateForRegistration(UserForRegistrationDto userForRegistration)
        {
            Validate(userForRegistration);

            var existingUser = await _uow.Repository<User>()
                .FindOneAsync(new UserWithRolesSpecification(userForRegistration.Username));

            if (existingUser != null)
                AddValidationError("Username", "Already exists!");

            ThrowValidationErrorsIfNotEmpty();
        }
    }
}

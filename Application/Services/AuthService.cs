using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.User.Specifications;
using Ardalis.GuardClauses;
using Common.Enums;
using Common.Exceptions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidatorService _validatorService;

        public AuthService(IUnitOfWork uow, IValidatorService validatorService)
        {
            _uow = uow;
            _validatorService = validatorService;
        }

        public async Task Login(UserForLoginDto userForLogin)
        {
            _validatorService.Validate(userForLogin);

            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserSpecification(userForLogin.Username));
            
            Guard.Against.EntityNotFound(user, nameof(user));

            if (!VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedException();
        }

        public async Task Register(UserForRegistrationDto userForRegistration)
        {
            _validatorService.Validate(userForRegistration);

            var user = await _uow.Repository<Domain.Entities.User>()
                .FindOneAsync(new UserSpecification(userForRegistration.Username));

            if (user != null)
                _validatorService.ThrowValidationError("Username", "Username already exists!");

            CreatePasswordHash(userForRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // TODO: use autommaper
            var userToRegister = new Domain.Entities.User
            {
                Username = userForRegistration.Username,
                Email = userForRegistration.Email,                
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = Status.Activated
            };

            _uow.Repository<Domain.Entities.User>().Add(userToRegister);
            await _uow.SaveAsync();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

    }
}

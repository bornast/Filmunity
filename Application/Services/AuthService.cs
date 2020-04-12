using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.User.Specifications;
using Ardalis.GuardClauses;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using Domain.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidatorService _validatorService;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork uow, IValidatorService validatorService, IMapper mapper)
        {
            _uow = uow;
            _validatorService = validatorService;
            _mapper = mapper;
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

            var existingUser = await _uow.Repository<Domain.Entities.User>()
                .FindOneAsync(new UserSpecification(userForRegistration.Username));

            if (existingUser != null)
                _validatorService.ThrowValidationError("Username", "Username already exists!");

            CreatePasswordHash(userForRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = _mapper.Map<Domain.Entities.User>(userForRegistration);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _uow.Repository<Domain.Entities.User>().Add(user);
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

using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.Specifications;
using Ardalis.GuardClauses;
using AutoMapper;
using Common.Enums;
using Domain.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHashService _hashService;

        public AuthService(IUnitOfWork uow, IMapper mapper, IJwtService jwtService, IHashService hashService)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashService = hashService;
        }

        public async Task<string> Login(UserForLoginDto userForLogin)
        {
            var user = await _uow.Repository<User>().FindOneAsync(new UserWithRolesSpecification(userForLogin.Username));

            Guard.Against.Unauthorized(user);
            Guard.Against.Unauthorized(_hashService.VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt));

            return _jwtService.GenerateJwtToken(user);
        }

        public async Task Register(UserForRegistrationDto userForRegistration)
        {            
            var user = _mapper.Map<User>(userForRegistration);

            var password = _hashService.CreatePasswordHash(userForRegistration.Password);
            user.PasswordHash = password.PasswordHash;
            user.PasswordSalt = password.PasswordSalt;

            user.Roles.Add(new UserRole { RoleId = (int)Roles.User });

            _uow.Repository<User>().Add(user);
            await _uow.SaveAsync();
        }        

    }
}

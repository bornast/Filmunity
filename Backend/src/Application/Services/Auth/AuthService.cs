using Application.Dtos.Common;
using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Specifications;
using Ardalis.GuardClauses;
using AutoMapper;
using Common.Enums;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IHashService _hashService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IUnitOfWork uow, IMapper mapper, IJwtService jwtService, IHashService hashService, IRefreshTokenService refreshTokenService)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashService = hashService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<TokenDto> Login(UserForLoginDto userForLogin)
        {
            var user = await _uow.Repository<User>().FindOneAsync(new UserWithRolesSpecification(userForLogin.Username));

            Guard.Against.Unauthorized(user);
            Guard.Against.Unauthorized(_hashService.VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt));

            return await _jwtService.GenerateJwtToken(user);
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

        public async Task<TokenDto> RefreshToken(TokenDto tokenForRefresh)
        {
            var validatedToken = _jwtService.GetPrincipalFromToken(tokenForRefresh.Token);

            var jti = _jwtService.GetJtiFromToken(validatedToken);

            await _refreshTokenService.MarkAsUsed(tokenForRefresh.RefreshToken, jti, commit: false);

            var user = await _uow.Repository<User>().FindOneAsync(new UserWithRolesSpecification(_jwtService.GetUserIdFromToken(validatedToken)));

            return await _jwtService.GenerateJwtToken(user);
        }

    }
}

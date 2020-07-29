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
using System;
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
        private readonly IFacebookService _facebookService;

        public AuthService(IUnitOfWork uow, IMapper mapper, IJwtService jwtService, IHashService hashService, IRefreshTokenService refreshTokenService, IFacebookService facebookService)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtService = jwtService;
            _hashService = hashService;
            _refreshTokenService = refreshTokenService;
            _facebookService = facebookService;
        }

        public async Task<TokenDto> Login(UserForLoginDto userForLogin)
        {
            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(userForLogin.Username));

            Guard.Against.Unauthorized(user);
            Guard.Against.Unauthorized(_hashService.VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt));

            return await _jwtService.GenerateJwtToken(user);
        }

        public async Task Register(UserForRegistrationDto userForRegistration)
        {            
            var user = _mapper.Map<Domain.Entities.User>(userForRegistration);

            var password = _hashService.CreatePasswordHash(userForRegistration.Password);
            user.PasswordHash = password.PasswordHash;
            user.PasswordSalt = password.PasswordSalt;

            user.Roles.Add(new UserRole { RoleId = (int)Roles.User });

            _uow.Repository<Domain.Entities.User>().Add(user);
            await _uow.SaveAsync();
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenForRefresh)
        {
            var validatedToken = _jwtService.GetPrincipalFromToken(tokenForRefresh.Token);

            var jti = _jwtService.GetJtiFromToken(validatedToken);

            await _refreshTokenService.MarkAsUsed(tokenForRefresh.RefreshToken, jti, commit: false);

            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(_jwtService.GetUserIdFromToken(validatedToken)));

            return await _jwtService.GenerateJwtToken(user);
        }

        public async Task<TokenDto> LoginWithFacebook(FacebookLoginDto facebookLogin)
        {
            var userInfo = await _facebookService.GetUserInfoAsync(facebookLogin.AccessToken);

            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(username: userInfo.Email));

            if (user == null)
            {
                var userToRegister = _mapper.Map<UserForRegistrationDto>(userInfo);

                await Register(userToRegister);

                user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(username: userInfo.Email));
            }

            return await _jwtService.GenerateJwtToken(user);
        }
    }
}

﻿using Application.Dtos.Common;
using Application.Dtos.User;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Specifications;
using Application.Specifications.Common;
using Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthValidatorService : BaseValidatorService, IAuthValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IJwtService _jwtService;
        private readonly IFacebookService _facebookService;

        public AuthValidatorService(
            IUnitOfWork uow, 
            IValidatorFactoryService validatorFactoryService,
            IJwtService jwtService,
            IFacebookService facebookService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _jwtService = jwtService;
            _facebookService = facebookService;
        }
        
        public void ValidateForLogin(UserForLoginDto userForLogin)
        {
            Validate(userForLogin);
        }
        
        public async Task ValidateForRegistration(UserForRegistrationDto userForRegistration)
        {
            Validate(userForRegistration);

            var existingUser = await _uow.Repository<Domain.Entities.User>()
                .FindOneAsync(new UserWithRolesSpecification(userForRegistration.Username));

            if (existingUser != null)
                AddValidationError("Username", "Already exists!");

            ThrowValidationErrorsIfNotEmpty();
        }
        public async Task ValidateBeforeTokenRefresh(TokenDto tokenForRefresh)
        {
            var validatedToken = _jwtService.GetPrincipalFromToken(tokenForRefresh.Token);

            if (validatedToken == null)
                AddValidationError("Token", "Invalid token!");

            ThrowValidationErrorsIfNotEmpty();

            var expiryDateUnix = long.Parse(validatedToken.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                AddValidationError("Token", "Invalid token!");

            ThrowValidationErrorsIfNotEmpty();

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _uow.Repository<RefreshToken>()
                .FindOneAsync(new RefreshTokenFilterSpecification(jti, tokenForRefresh.RefreshToken));

            if (storedRefreshToken == null
                || DateTime.UtcNow > storedRefreshToken.ExpiryDate
                || storedRefreshToken.Invalidated
                || storedRefreshToken.Used)
                AddValidationError("Token", "Invalid token!");

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateForLoginWithFacebook(FacebookLoginDto facebookLogin)
        {
            Validate(facebookLogin);

            var isTokenValid = await _facebookService.ValidateAccessTokenAsync(facebookLogin.AccessToken);

            if (isTokenValid == false)
                AddValidationError("Token", "Invalid token!");

            ThrowValidationErrorsIfNotEmpty();
        }

        public void ValidateForLoginWithTwitter(TwitterLoginDto twitterLogin)
        {
            Validate(twitterLogin);

            ThrowValidationErrorsIfNotEmpty();
        }
    }
}

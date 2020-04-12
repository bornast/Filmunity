using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.User.Specifications;
using Ardalis.GuardClauses;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public AuthService(IUnitOfWork uow, IMapper mapper, IJwtService jwtService)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<string> Login(UserForLoginDto userForLogin)
        {
            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(userForLogin.Username));

            Guard.Against.Unauthorized(user);

            var passwordVerified = VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt);
            Guard.Against.Unauthorized(passwordVerified);

            return _jwtService.GenerateJwtToken(user);
        }

        public async Task Register(UserForRegistrationDto userForRegistration)
        {            
            var user = _mapper.Map<Domain.Entities.User>(userForRegistration);

            CreatePasswordHash(userForRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);
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

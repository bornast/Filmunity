using Application.Dtos.User;
using Application.Interfaces;
using Application.Specifications;
using Common.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;

        public AuthService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Login(UserForLoginDto userForLogin)
        {
            var user = _uow.Repository<User>().Find(new UserSpecification(userForLogin.Username)).FirstOrDefault();

            if (user == null)
                throw new Exception();

            if (!VerifyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt))
                throw new Exception();
        }

        public void Register(UserForRegistrationDto userForRegistration)
        {
            CreatePasswordHash(userForRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userToRegister = new User
            {
                Username = userForRegistration.Username,
                Email = userForRegistration.Email,                
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = Status.Activated
            };

            _uow.Repository<User>().Add(userToRegister);
            var result = _uow.Save();
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

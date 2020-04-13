using Application.Helpers;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class HashService : IHashService
    {
        public Password CreatePasswordHash(string password)
        {
            var passwordObject = new Password();

            using var hmac = new HMACSHA512();

            passwordObject.PasswordSalt = hmac.Key;
            passwordObject.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return passwordObject;
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
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

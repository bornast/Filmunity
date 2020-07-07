using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Specifications.Common;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Application.Services.Common
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public RefreshTokenService(IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public async Task MarkAsUsed(string refreshToken, string jti, bool commit = false)
        {
            var storedRefreshToken = await _uow.Repository<RefreshToken>()
                .FindOneAsync(new RefreshTokenFilterSpecification(jti, refreshToken));

            storedRefreshToken.Used = true;

            if (commit)
                await _uow.SaveAsync();
        }

        public async Task MarkAsInvalid(string jti, bool commit = false)
        {
            var refreshTokens = await _uow.Repository<RefreshToken>()
                .FindAsync(new RefreshTokenFilterSpecification(jti, isInvalidated: false, isUsed: true));

            foreach (var refreshToken in refreshTokens)
            {
                refreshToken.Invalidated = true;
            }

            if (commit)
                await _uow.SaveAsync();
        }

        public async Task<RefreshToken> CreateRefreshToken(string jti, User user, bool commit = false)
        {
            var refreshToken = new RefreshToken
            {
                JwtId = jti,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration.GetSection("RefreshTokenExpireMinutes").Value))
            };

            _uow.Repository<RefreshToken>().Add(refreshToken);

            if (commit)
                await _uow.SaveAsync();

            return refreshToken;
        }
    }
}

using Application.Dtos.Common;
using Application.Interfaces;
using Application.Interfaces.Common;
using Ardalis.GuardClauses;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenService _refreshTokenService;

        public JwtService(IConfiguration configuration, TokenValidationParameters tokenValidationParameters, IRefreshTokenService refreshTokenService)
        {
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokenService = refreshTokenService;
        }

        public string GetJtiFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        }

        public int GetUserIdFromToken(ClaimsPrincipal claimsPrincipal)
        {
            return Convert.ToInt32(claimsPrincipal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }

        public async Task<TokenDto> GenerateJwtToken(User user)
        {
            Guard.Against.Null(user, nameof(user));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("username", user.Username)
            };

            var roles = user.Roles.Select(x => x.Role).ToList();

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtExpireMinutes"]));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var refreshToken = await _refreshTokenService.CreateRefreshToken(token.Id, user, commit: true);

            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.Token
            };
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = validateLifetime;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature,
                StringComparison.InvariantCultureIgnoreCase);
        }

    }
}

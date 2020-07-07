using Application.Dtos.Common;
using Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        Task<TokenDto> GenerateJwtToken(User user);
        ClaimsPrincipal GetPrincipalFromToken(string token, bool validateLifetime = false);
        string GetJtiFromToken(ClaimsPrincipal claimsPrincipal);
        int GetUserIdFromToken(ClaimsPrincipal claimsPrincipal);
    }
}

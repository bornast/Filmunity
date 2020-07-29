using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface IRefreshTokenService
    {
        Task MarkAsUsed(string refreshToken, string jti, bool commit = false);
        Task MarkAsInvalid(string jti, bool commit = false);
        Task<RefreshToken> CreateRefreshToken(string jti, Domain.Entities.User user, bool commit = false);
    }
}

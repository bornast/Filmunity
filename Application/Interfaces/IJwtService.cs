using Domain.Entities;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}

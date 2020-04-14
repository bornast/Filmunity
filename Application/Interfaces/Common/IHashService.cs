using Application.Models;

namespace Application.Interfaces
{
    public interface IHashService
    {
        Password CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}

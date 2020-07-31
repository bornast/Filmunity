using Common.Enums;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : BaseEntity
    {        
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Interests { get; set; }
        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<Friendship> Senders { get; set; }
        public ICollection<Friendship> Receivers { get; set; }
    }
}

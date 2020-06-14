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
        public string Remark { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserRole
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}

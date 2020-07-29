using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserForUpdateDto : IObjectToValidate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Interests { get; set; }
        public ICollection<int> RoleIds { get; set; } = new List<int>();
    }
}

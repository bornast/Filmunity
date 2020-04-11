using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserForRegistrationDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

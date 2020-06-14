using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserForLoginDto : IObjectToValidate
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

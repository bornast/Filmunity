using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Common
{
    public class FacebookLoginDto : IObjectToValidate
    {
        public string AccessToken { get; set; }
    }
}

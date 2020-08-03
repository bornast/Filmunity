using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Common
{
    public class TwitterLoginDto : IObjectToValidate
    {
        public string OAuthToken { get; set; }
        public string OAuthVerifier { get; set; }
    }
}

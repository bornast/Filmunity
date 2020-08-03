using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class TwitterTokenResponse
    {
        [JsonProperty("oauth_token")]
        public string OAuthToken { get; set; }

        [JsonProperty("oauth_token_secret")]
        public string OAuthTokenSecret { get; set; }

        [JsonProperty("oauth_callback_confirmed")]
        public bool OAuthCallbackConfirmed { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class TwitterSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
        public string CallbackUrl { get; set; }
    }
}

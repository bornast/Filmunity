using Application.Dtos.Common;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface ITwitterService
    {
        TwitterTokenResponse GetRequestToken();
        TwitterUser GetUserInfo(TwitterLoginDto twitterLogin);
    }
}

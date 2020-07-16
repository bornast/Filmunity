using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Common
{
    public interface IFacebookService
    {
        Task<bool> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUser> GetUserInfoAsync(string accessToken);
    }
}

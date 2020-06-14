using Application.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {            
            var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = userId != null;

            UserId = IsAuthenticated ? Convert.ToInt32(userId) : 0;
        }

        public int? UserId { get; }

        public bool IsAuthenticated { get; }
    }
}

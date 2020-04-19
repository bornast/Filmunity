using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Common
{
    public interface ICurrentUserService
    {
        int? UserId { get; }

        bool IsAuthenticated { get; }
    }
}

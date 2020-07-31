using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Friendship
{
    public interface IFriendshipValidatorService
    {
        Task ValidateFriendRequest(int userId);
        Task ValidateAcceptOrDeclineFriendRequest(int userId);
    }
}

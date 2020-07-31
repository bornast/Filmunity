using Application.Dtos.Friendship;
using Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Friendship
{
    public interface IFriendshipService
    {
        Task<IEnumerable<FriendRequestForListDto>> GetAllFriendRequests();
        Task<IEnumerable<FriendDto>> GetAllFriends();
        Task SendFriendRequest(int userId);
        Task AcceptFriendRequest(int userId);
        Task DeclineFriendRequest(int userId);
    }
}

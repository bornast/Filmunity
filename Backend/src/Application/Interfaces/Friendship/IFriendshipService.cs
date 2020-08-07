using Application.Dtos.Common;
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
        Task<IEnumerable<FriendRequestForListDto>> GetAllFriendshipRequests();
        Task<IEnumerable<FriendDto>> GetAllFriends(FriendshipFilterDto friendshipFilter);
        Task SendFriendRequest(int userId);
        Task AcceptFriendRequest(int userId);
        Task DeclineFriendRequest(int userId);
        Task<RecordNameDto> GetFriendShipStatus(int userId);
    }
}

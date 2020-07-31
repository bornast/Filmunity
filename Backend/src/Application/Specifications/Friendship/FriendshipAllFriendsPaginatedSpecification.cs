using Application.Dtos.Friendship;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Friendship
{
    public class FriendshipAllFriendsPaginatedSpecification : BaseSpecification<Domain.Entities.Friendship>
    {
        public FriendshipAllFriendsPaginatedSpecification(FriendshipFilterDto friendshipFilter)
            : base(x => (x.ReceiverId == friendshipFilter.UserId || x.SenderId == friendshipFilter.UserId) 
            && x.StatusId == (int)FriendshipStatus.Accepted)
        {
            AddInclude($"{nameof(Domain.Entities.Friendship.Receiver)}");
            AddInclude($"{nameof(Domain.Entities.Friendship.Sender)}");
            ApplyPaging(friendshipFilter.Skip, friendshipFilter.Take, friendshipFilter.PageNumber);
        }
    }
}

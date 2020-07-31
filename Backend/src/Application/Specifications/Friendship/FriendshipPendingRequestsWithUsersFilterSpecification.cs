using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Friendship
{

    public class FriendshipPendingRequestsWithUsersFilterSpecification : BaseSpecification<Domain.Entities.Friendship>
    {
        public FriendshipPendingRequestsWithUsersFilterSpecification(int userId)
            : base(x => x.ReceiverId == userId && x.StatusId == (int)FriendshipStatus.Sent)
        {
            AddInclude($"{nameof(Domain.Entities.Friendship.Sender)}");
        }
    }
}

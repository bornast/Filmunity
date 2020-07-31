using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Friendship
{
    public class FriendshipFilterSpecification : BaseSpecification<Domain.Entities.Friendship>
    {
        public FriendshipFilterSpecification(int senderId, int receiverId)
            : base(x => (x.SenderId == senderId && x.ReceiverId == receiverId) 
            || (x.SenderId == receiverId && x.ReceiverId == senderId))
        {
        }
    }
}

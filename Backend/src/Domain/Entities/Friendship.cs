using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Friendship : BaseEntity
    {
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ReceiverId { get; set; }
        public User Receiver { get; set; }
        public FriendshipStatus Status { get; set; }
        public int StatusId { get; set; }
    }
}

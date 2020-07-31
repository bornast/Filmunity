using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Friendship
{
    public class FriendshipFilterDto : BaseFilter
    {
        public int UserId { get; set; }
    }
}

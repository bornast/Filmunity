using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.User
{
    public class UserFilterDto : BaseFilter
    {
        public string Name { get; set; }
        public int ExcludeUserId { get; set; }
    }
}

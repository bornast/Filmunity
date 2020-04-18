using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Review
    {
        public Film Film { get; set; }
        public int FilmId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FilmComment : BaseEntity
    {
        public Film Film { get; set; }
        public int FilmId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

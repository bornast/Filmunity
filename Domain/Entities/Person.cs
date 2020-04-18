using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Person : BaseEntity, ITrackable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }
        public int GenderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public User CreatedByUser { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public User ModifiedByUser { get; set; }
        public int? ModifiedByUserId { get; set; }
    }
}

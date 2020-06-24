using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Photo : BaseEntity
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public EntityType EntityType { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
    }
}

using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ITrackable
    {
        DateTime CreatedAt { get; set; }

        User CreatedByUser { get; set; }

        int CreatedByUserId { get; set; }

        DateTime? ModifiedAt { get; set; }

        User ModifiedByUser { get; set; }

        int? ModifiedByUserId { get; set; }
    }
}

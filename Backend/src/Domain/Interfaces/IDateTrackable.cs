using System;

namespace Domain.Interfaces
{
    public interface IDateTrackable
    {
        DateTime CreatedAt { get; set; }

        DateTime? ModifiedAt { get; set; }
    }
}

using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITrackable : IDateTrackable
    {
        User CreatedByUser { get; set; }

        int CreatedByUserId { get; set; }

        User ModifiedByUser { get; set; }

        int? ModifiedByUserId { get; set; }
    }
}

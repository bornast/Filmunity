using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specifications.Photo
{
    public class PhotoFilterSpecification : BaseSpecification<Domain.Entities.Photo>
    {
        public PhotoFilterSpecification(int entityTypeId, int entityId, bool isMain)
            : base(x => x.IsMain == isMain && x.EntityTypeId == entityTypeId && x.EntityId == entityId)
        {
        }

        public PhotoFilterSpecification(int entityTypeId, int entityId)
            : base(x => x.EntityTypeId == entityTypeId && x.EntityId == entityId)
        {
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.EntityType
{
    public interface IEntityTypeService
    {
        Task<bool> EntityExistsForEntityType(int entityTypeId, int entityId);
    }
}

using Application.Interfaces;
using Application.Interfaces.EntityType;
using Common.Enums;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.EntityType
{
    public class EntityTypeService : IEntityTypeService
    {
        private readonly IUnitOfWork _uow;

        public EntityTypeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> EntityExistsForEntityType(int entityTypeId, int entityId)
        {
            if (entityTypeId == (int)EntityTypes.Film)
                return await _uow.Repository<Film>().FindByIdAsync(entityId) != null;
            else if (entityTypeId == (int)EntityTypes.Person)
                return await _uow.Repository<Person>().FindByIdAsync(entityId) != null;
            else if (entityTypeId == (int)EntityTypes.User)
                return await _uow.Repository<User>().FindByIdAsync(entityId) != null;
            else if (entityTypeId == (int)EntityTypes.Watchlist)
                return await _uow.Repository<Watchlist>().FindByIdAsync(entityId) != null;

            return false;
        }

    }
}

using Application.Dtos.Photo;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.EntityType;
using Application.Interfaces.Photo;
using Application.Specifications;
using Common.Enums;
using Common.Exceptions;
using Common.Libs;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Photo
{
    public class PhotoValidatorService : BaseValidatorService, IPhotoValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEntityTypeService _entityTypeService;

        public PhotoValidatorService(
            IUnitOfWork uow,
            ICurrentUserService currentUserService,
            IValidatorFactoryService validatorFactoryService,            
            IEntityTypeService entityTypeService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
            _entityTypeService = entityTypeService;
        }

        public async Task ValidateForUpload(PhotoForCreationDto photoForCreation)
        {
            Validate(photoForCreation);

            ValidateEntityType(photoForCreation.EntityTypeId);

            await ValidateEntityId(photoForCreation.EntityTypeId, photoForCreation.EntityId);

            ThrowValidationErrorsIfNotEmpty();

            ValidateUserPhotoPermission(photoForCreation.EntityTypeId, photoForCreation.EntityId);

            await ValidateWatchlistPhotoPermission(photoForCreation.EntityTypeId, photoForCreation.EntityId);

            await ValidateFilmOrPersonPhotoPermission(photoForCreation.EntityTypeId, photoForCreation.EntityId);
        }

        public async Task ValidateForDeletion(int photoId)
        {
            var photo = await _uow.Repository<Domain.Entities.Photo>().FindByIdAsync(photoId);
            AddValidationErrorIfValueIsNull(photo, "Photo", $"Id {photoId} not found");

            ValidateUserPhotoPermission(photo.EntityTypeId, photo.EntityId);

            await ValidateWatchlistPhotoPermission(photo.EntityTypeId, photo.EntityId);

            await ValidateFilmOrPersonPhotoPermission(photo.EntityTypeId, photo.EntityId);
        }

        public async Task ValidateForSetMain(int photoId)
        {
            var photo = await _uow.Repository<Domain.Entities.Photo>().FindByIdAsync(photoId);
            AddValidationErrorIfValueIsNull(photo, "Photo", $"Id {photoId} not found");

            ValidateUserPhotoPermission(photo.EntityTypeId, photo.EntityId);

            await ValidateWatchlistPhotoPermission(photo.EntityTypeId, photo.EntityId);

            await ValidateFilmOrPersonPhotoPermission(photo.EntityTypeId, photo.EntityId);
        }

        #region private methods

        private void ValidateEntityType(int entityType)
        {
            if (!EnumLibrary.GetIntValuesFromEnumType(typeof(EntityTypes)).Contains(entityType))
                AddValidationError("Photo", $"EntityTypeId {entityType} is not valid!");
        }

        private async Task ValidateEntityId(int entityTypeId, int entityId)
        {
            if (!await _entityTypeService.EntityExistsForEntityType(entityTypeId, entityId))
            {
                AddValidationError("EntityId", $"EntityId {entityId} for EntityTypeId {entityTypeId} doesn't exist!");
            }
        }

        private void ValidateUserPhotoPermission(int entityTypeId, int entityId)
        {
            if (entityTypeId == (int)EntityTypes.User && _currentUserService.UserId != entityId)
            {
                throw new ForbiddenException();
            }
        }

        private async Task ValidateWatchlistPhotoPermission(int entityTypeId, int entityId)
        {
            if (entityTypeId == (int)EntityTypes.Watchlist &&
                _currentUserService.UserId != 
                (await _uow.Repository<Domain.Entities.Watchlist>().FindByIdAsync(entityId)).UserId)
            {
                throw new ForbiddenException();
            }
        }

        private async Task ValidateFilmOrPersonPhotoPermission(int entityTypeId, int entityId)
        {
            if (entityTypeId == (int)EntityTypes.Film || entityId == (int)EntityTypes.Person)
            {
                var user = await _uow.Repository<User>().FindOneAsync(new UserWithRolesSpecification((int)_currentUserService.UserId));

                var validRoles = new List<int>
                {
                    (int)Roles.Admin,
                    (int)Roles.Moderator
                };

                if (!user.Roles.Any(x => validRoles.Contains(x.RoleId)))
                    throw new ForbiddenException();
            }
            
        }

        #endregion
    }
}

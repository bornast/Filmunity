using Application.Dtos.Photo;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.EntityType;
using Application.Interfaces.Photo;
using Common.Enums;
using Common.Exceptions;
using Common.Libs;
using Domain.Entities;
using System.Collections.Generic;
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

            // check if entitytype is valid
            if (!EnumLibrary.GetIntValuesFromEnumType(typeof(EntityTypes)).Contains(photoForCreation.EntityTypeId))
                AddValidationError("Photo", $"EntityTypeId {photoForCreation.EntityTypeId} is not valid!");

            // check if the entity type id sent in the request exists in the database
            if (!await _entityTypeService.EntityExistsForEntityType(
                photoForCreation.EntityTypeId, photoForCreation.EntityId))
            {
                AddValidationError("EntityId", $"EntityId {photoForCreation.EntityId} for EntityTypeId {photoForCreation.EntityTypeId} doesn't exist!");
            }

            ThrowValidationErrorsIfNotEmpty();

            // check if user is uploading a photo for himself or his watchlist
            if (photoForCreation.EntityTypeId == (int)EntityTypes.User &&
                _currentUserService.UserId != photoForCreation.EntityId)
            {
                throw new UnauthorizedException();
            }

            if (photoForCreation.EntityTypeId == (int)EntityTypes.Watchlist &&
                _currentUserService.UserId != 
                (await _uow.Repository<Watchlist>().FindByIdAsync(photoForCreation.EntityId)).UserId)
            {
                throw new UnauthorizedException();
            }            
        }
    }
}

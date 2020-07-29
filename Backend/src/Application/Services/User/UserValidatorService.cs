using Application.Dtos.User;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.User;
using Application.Specifications;
using Common.Enums;
using Common.Exceptions;
using Common.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public class UserValidatorService : BaseValidatorService, IUserValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public UserValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow,
            ICurrentUserService currentUserService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task ValidateForUpdate(int id, UserForUpdateDto userForUpdate)
        {
            Validate(userForUpdate);

            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(id));
            AddValidationErrorIfValueIsNull(user, "User", $"Id {id} not found");

            AddValidationErrorIfIdDoesntExist(userForUpdate.RoleIds.ToList(), EnumLibrary.GetIntValuesFromEnumType(typeof(Roles)), "Role", "Id __id__ not found");

            ThrowValidationErrorsIfNotEmpty();
            
            var loggedUser = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification((int)_currentUserService.UserId));
            var isLoggedUserAdmin = loggedUser.Roles.Any(x => x.RoleId == (int)Roles.Admin);

            var roles = user.Roles.Select(x => x.Role);

            /* if the logged user is not admin and is trying to update someones roles
            or the user is trying to update his roles, throw 403 forbidden */
            if ((!isLoggedUserAdmin || _currentUserService.UserId == id)
                && (!roles.Select(x => x.Id).All(userForUpdate.RoleIds.Contains) 
                || roles.Count() != userForUpdate.RoleIds.Count()))
                throw new ForbiddenException();
        }
    }
}

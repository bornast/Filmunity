using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Friendship;
using Application.Specifications.Friendship;
using AutoMapper.QueryableExtensions;
using Common.Enums;
using Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Friendship
{
    public class FriendshipValidatorService : BaseValidatorService, IFriendshipValidatorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUserService;

        public FriendshipValidatorService(
            IValidatorFactoryService validatorFactoryService,
            IUnitOfWork uow,
            ICurrentUserService currentUserService
        ) : base(validatorFactoryService)
        {
            _uow = uow;
            _currentUserService = currentUserService;
        }

        public async Task ValidateFriendRequest(int userId)
        {
            if (userId == (int)_currentUserService.UserId)
                throw new BadRequestException();

            var friendshipSpecification = new FriendshipFilterSpecification((int)_currentUserService.UserId, userId);

            var friendship = await _uow.Repository<Domain.Entities.Friendship>().FindOneAsync(friendshipSpecification);

            if (friendship != null)
            {
                if (friendship.StatusId == (int)FriendshipStatus.Accepted)
                    AddValidationError("Friendship", $"You are already friends!");
                else if (friendship.StatusId == (int)FriendshipStatus.Declined)
                    AddValidationError("Friendship", $"Friendship is already declined!");
                else if (friendship.StatusId == (int)FriendshipStatus.Sent)
                    AddValidationError("Friendship", $"Friendship request is already sent!");
            }

            ThrowValidationErrorsIfNotEmpty();
        }

        public async Task ValidateAcceptOrDeclineFriendRequest(int userId)
        {
            var friendshipSpecification = new FriendshipFilterSpecification((int)_currentUserService.UserId, userId);

            var friendship = await _uow.Repository<Domain.Entities.Friendship>().FindOneAsync(friendshipSpecification);
            AddValidationErrorIfValueIsNull(friendship, "Friendship", $"Friendship doesnt't exist!");

            ThrowValidationErrorsIfNotEmpty();

            var loggedUserId = (int)_currentUserService.UserId;

            if (friendship.SenderId == loggedUserId || friendship.StatusId != (int)FriendshipStatus.Sent)
                throw new BadRequestException();
        }

    }    
}

using Application.Dtos.Friendship;
using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Friendship;
using Application.Interfaces.Photo;
using Application.Specifications.Friendship;
using AutoMapper;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Friendship
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _context;

        public FriendshipService(IUnitOfWork uow,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IPhotoService photoService,
            IHttpContextAccessor context)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
            _context = context;
        }

        public async Task<IEnumerable<FriendRequestForListDto>> GetAllFriendRequests()
        {
            var friendshipSpec = new FriendshipPendingRequestsWithUsersFilterSpecification((int)_currentUserService.UserId);

            var friendships = await _uow.Repository<Domain.Entities.Friendship>().FindAsync(friendshipSpec);

            var friendshipRequestsToReturn = _mapper.Map<IEnumerable<FriendRequestForListDto>>(friendships);

            await _photoService.IncludeMainPhoto(friendshipRequestsToReturn, (int)EntityTypes.User);

            return friendshipRequestsToReturn;
        }

        public async Task<IEnumerable<FriendDto>> GetAllFriends()
        {
            var friendshipSpec = new FriendshipAllFriendsPaginatedSpecification(new FriendshipFilterDto { UserId = (int)_currentUserService.UserId});

            var friendships = await _uow.Repository<Domain.Entities.Friendship>().FindAsyncWithPagination(friendshipSpec);

            _context.HttpContext.Response.AddPagination(friendships.CurrentPage, friendships.PageSize, friendships.TotalCount, friendships.TotalPages);

            var result = new List<FriendDto>();

            foreach (var friendship in friendships)
            {

                if ((int)_currentUserService.UserId == friendship.SenderId)
                {
                    result.Add(new FriendDto
                    {
                        Id = friendship.ReceiverId,
                        Username = friendship.Receiver.Username,
                        FirstName = friendship.Receiver.FirstName,
                        LastName = friendship.Receiver.LastName,
                        Interests = friendship.Receiver.Interests
                    });                    
                }                    
                else
                {
                    result.Add(new FriendDto
                    {
                        Id = friendship.SenderId,
                        Username = friendship.Sender.Username,
                        FirstName = friendship.Sender.FirstName,
                        LastName = friendship.Sender.LastName,
                        Interests = friendship.Sender.Interests
                    });
                }
            }            

            await _photoService.IncludeMainPhoto(result, (int)EntityTypes.User);

            return result;
        }

        public async Task SendFriendRequest(int userId)
        {
            var friendship = new Domain.Entities.Friendship
            {
                ReceiverId = userId,
                SenderId = (int)_currentUserService.UserId,
                StatusId = (int)FriendshipStatus.Sent
            };

            _uow.Repository<Domain.Entities.Friendship>().Add(friendship);

            await _uow.SaveAsync();
        }

        public async Task AcceptFriendRequest(int userId)
        {
            var friendshipSpecification = new FriendshipFilterSpecification((int)_currentUserService.UserId, userId);

            var friendship = await _uow.Repository<Domain.Entities.Friendship>().FindOneAsync(friendshipSpecification);

            friendship.StatusId = (int)FriendshipStatus.Accepted;

            await _uow.SaveAsync();
        }

        public async Task DeclineFriendRequest(int userId)
        {
            var friendshipSpecification = new FriendshipFilterSpecification((int)_currentUserService.UserId, userId);

            var friendship = await _uow.Repository<Domain.Entities.Friendship>().FindOneAsync(friendshipSpecification);

            friendship.StatusId = (int)FriendshipStatus.Declined;

            await _uow.SaveAsync();
        }
    }

}
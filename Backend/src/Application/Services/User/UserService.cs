using Application.Dtos.User;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Photo;
using Application.Interfaces.User;
using Application.Specifications;
using Application.Specifications.User;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _context;

        public UserService(IUnitOfWork uow, IMapper mapper, ICurrentUserService currentUserService, IPhotoService photoService, IHttpContextAccessor context)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
            _context = context;
        }

        public async Task<UserForDetailedDto> GetOne(int id)
        {
            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(id));

            if (user == null)
                return null;

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            await _photoService.IncludePhotos(userToReturn, (int)EntityTypes.User);

            return userToReturn;
        }

        public async Task<IEnumerable<UserForListDto>> GetAll(UserFilterDto userFilter)
        {
            userFilter.ExcludeUserId = (int)_currentUserService.UserId;
            var users = await _uow.Repository<Domain.Entities.User>().FindAsyncWithPagination(new UserWithRolesFilterPaginatedSpecification(userFilter));

            _context.HttpContext.Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            await _photoService.IncludeMainPhoto(usersToReturn, (int)EntityTypes.User);

            return usersToReturn;
        }        

        public async Task<UserForDetailedDto> Update(int id, UserForUpdateDto userForUpdate)
        {
            var user = await _uow.Repository<Domain.Entities.User>().FindOneAsync(new UserWithRolesSpecification(id));

            _mapper.Map(userForUpdate, user);

            await _uow.SaveAsync();

            return await GetOne(user.Id);
        }

        public async Task Delete(int id)
        {
            var user = await _uow.Repository<Domain.Entities.User>().FindByIdAsync(id);

            if (user == null)
                throw new NotFoundException(nameof(Domain.Entities.User));

            _uow.Repository<Domain.Entities.User>().Remove(user);

            await _uow.SaveAsync();
        }

    }
}

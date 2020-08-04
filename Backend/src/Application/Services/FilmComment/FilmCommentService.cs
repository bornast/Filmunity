using Application.Dtos.FilmComment;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.FilmComment;
using Application.Interfaces.Photo;
using Application.Specifications.FilmComment;
using AutoMapper;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.FilmComment
{
    public class FilmCommentService : IFilmCommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _context;

        public FilmCommentService(IUnitOfWork uow, IMapper mapper, ICurrentUserService currentUserService, IPhotoService photoService, IHttpContextAccessor context)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
            _context = context;
        }

        public async Task<IEnumerable<FilmCommentForListDto>> GetAll(FilmCommentFilterDto filmCommentFilter)
        {
            var filmComments = await _uow.Repository<Domain.Entities.FilmComment>().FindAsyncWithPagination(new FilmCommentWithUsersFilterPaginatedSpecification(filmCommentFilter));

            _context.HttpContext.Response.AddPagination(filmComments.CurrentPage, filmComments.PageSize, filmComments.TotalCount, filmComments.TotalPages);

            var filmCommentsToReturn = _mapper.Map<IEnumerable<FilmCommentForListDto>>(filmComments);

            await _photoService.IncludeMainPhoto(filmCommentsToReturn.Select(x => x.User), (int)EntityTypes.User);

            return filmCommentsToReturn;
        }

        public async Task Create(FilmCommentForCreationDto filmCommentForCreation)
        {
            var filmComment = _mapper.Map<Domain.Entities.FilmComment>(filmCommentForCreation);

            filmComment.UserId = (int)_currentUserService.UserId;

            _uow.Repository<Domain.Entities.FilmComment>().Add(filmComment);

            await _uow.SaveAsync();
        }

    }
}

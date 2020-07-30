using Application.Dtos.Review;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Common;
using Application.Interfaces.Photo;
using Application.Interfaces.Review;
using Application.Specifications.Review;
using AutoMapper;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Review
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _context;

        public ReviewService(IUnitOfWork uow, IMapper mapper, ICurrentUserService currentUserService, IPhotoService photoService, IHttpContextAccessor context)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _photoService = photoService;
            _context = context;
        }

        public async Task<IEnumerable<ReviewForListDto>> GetAll(ReviewFilterDto reviewFilter)
        {
            var reviews = await _uow.Repository<Domain.Entities.Review>().FindAsyncWithPagination(new ReviewWithUsersFilterPaginatedSpecification(reviewFilter));

            _context.HttpContext.Response.AddPagination(reviews.CurrentPage, reviews.PageSize, reviews.TotalCount, reviews.TotalPages);

            var reviewsToReturn = _mapper.Map<IEnumerable<ReviewForListDto>>(reviews);

            await _photoService.IncludeMainPhoto(reviewsToReturn.Select(x => x.User), (int)EntityTypes.User);

            return reviewsToReturn;
        }

        public async Task Create(ReviewForCreationDto reviewForCreation)
        {
            var review = _mapper.Map<Domain.Entities.Review>(reviewForCreation);

            review.UserId = (int)_currentUserService.UserId;

            _uow.Repository<Domain.Entities.Review>().Add(review);

            await _uow.SaveAsync();
        }
        
    }

}

using Application.Dtos.Review;
using Application.Interfaces.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IReviewValidatorService _reviewValidatorService;

        public ReviewController(IReviewService reviewService, IReviewValidatorService reviewValidatorService)
        {
            _reviewService = reviewService;
            _reviewValidatorService = reviewValidatorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ReviewFilterDto reviewFilter)
        {
            return Ok(await _reviewService.GetAll(reviewFilter));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ReviewForCreationDto reviewForCreation)
        {
            await _reviewValidatorService.ValidateForCreation(reviewForCreation);

            await _reviewService.Create(reviewForCreation);

            return Ok();
        }

    }
}

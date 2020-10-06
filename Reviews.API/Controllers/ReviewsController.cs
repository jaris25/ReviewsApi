﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reviews.Data.Entities;
using Reviews.Data.Models;
using Reviews.Data.Services;
using System;
using System.Threading.Tasks;

namespace Reviews.API.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ReviewsController: ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemsController> _logger;
        private readonly IMapper _mapper;

        public ReviewsController(IItemRepository itemRepository, ILogger<ItemsController> logger, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("average/{rating}")]
        public async Task<IActionResult> GetReviewByAverageRating(double rating)
        {
            try
            {
                var items = await _itemRepository.GetByAverageReviewRatingGreaterThanAsync(rating);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting reviews average", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{name}/reviews")]
        public async Task<IActionResult> GetReviewByName(string name)
        {
            try
            {
                if (!_itemRepository.ItemExists(name))
                {
                    _logger.LogInformation($"Sorry, {name} item doesn't exist");
                    return NotFound();
                }
                var reviews = await _itemRepository.GetReviewsByNameAsync(name);
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting reviews for {name}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPost("{name}/reviews")]
        public async Task<IActionResult> CreateReview(string name, [FromBody] ReviewForCreationDto review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_itemRepository.ItemExists(name))
            {
                return NotFound();
            }

            var reviewForCreation = _mapper.Map<Review>(review);
            await _itemRepository.LeaveReviewAsync(name, reviewForCreation);

            return Ok();
        }
    }
}

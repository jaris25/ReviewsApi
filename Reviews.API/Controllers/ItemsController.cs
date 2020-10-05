using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reviews.Data.Services;
using System;
using System.Threading.Tasks;

namespace Reviews.API.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ILogger<ItemsController> _logger;

        public ItemsController(IItemRepository itemRepository, ILogger<ItemsController> logger)
        {
            _itemRepository = itemRepository;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetItem(string name)
        {
            var item = await _itemRepository.GetByNameAsync(name);
            return Ok(item);
        }

        [HttpGet("average/{rating}")]
        public async Task<IActionResult> GetReviewByAverageRating(double rating)
        {
            var items = await _itemRepository.GetByAverageReviewRatingAsync(rating);
            return Ok(items);
        }

        [HttpGet("{name}/ratings")]
        public async Task<IActionResult> GetReviewByName(string name)
        {
            try
            {
                if(!_itemRepository.ItemExists(name))
                {
                    _logger.LogInformation($"Sorry, {name} item doesn't exist");
                    return NotFound();
                }
                var reviews = await _itemRepository.GetReviewsByNameAsync(name);
                return Ok(reviews);
            }
            catch(Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting reviews for {name}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}

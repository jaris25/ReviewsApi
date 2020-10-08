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
            try
            {
                if (!_itemRepository.ItemExists(name))
                {
                    _logger.LogInformation($"Tried to acces {name}, this item doesn't exist");
                    return NotFound();
                }
                var item = await _itemRepository.GetByNameAsync(name);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occured while getting reviews for {name}", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}

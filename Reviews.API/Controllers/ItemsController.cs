using AutoMapper;
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
        private readonly IMapper _mapper;
        public ItemsController(IItemRepository itemRepository, ILogger<ItemsController> logger, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetItem(string name)
        {
            try
            {
                if (!_itemRepository.ItemExists(name))
                {
                    _logger.LogInformation($"Sorry, {name} item doesn't exist");
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

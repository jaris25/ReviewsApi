using Microsoft.AspNetCore.Mvc;
using Reviews.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.API.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController: ControllerBase
    {
        private readonly ReviewsContext _context;

        public ItemsController(ReviewsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult TEST()
        {
            return Ok();
        }
    }
}

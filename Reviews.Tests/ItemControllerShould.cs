using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Reviews.API.Controllers;
using Reviews.Data.Contexts;
using Reviews.Data.Entities;
using Reviews.Data.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Reviews.Tests
{
    public class ItemControllerShould
    {
        private readonly Mock<ILogger<ItemsController>> _mockLogger;

        public ItemControllerShould()
        {
            _mockLogger = new Mock<ILogger<ItemsController>>();
        }

        [Fact]
        public async Task GetItemReturnsItem()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item" });
                context.Items.Add(new Item { Name = "item1" });
                context.SaveChanges();
            }

            using (var context = new ReviewsContext(options))
            {
                ItemRepository repo = new ItemRepository(context);
                var _controller = new ItemsController(repo, _mockLogger.Object);
                var okResult = await _controller.GetItem("item") as OkObjectResult;
                Assert.Equal(200, okResult.StatusCode);
            }
        }
    }
}

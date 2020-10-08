using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Reviews.API.Controllers;
using Reviews.Data.Entities;
using Reviews.Data.Services;
using System.Threading.Tasks;
using Xunit;

namespace Reviews.Tests
{
    public class ItemControllerShould
    {
        private readonly Mock<ILogger<ItemsController>> _mockLogger;
        private readonly ItemsController _controller;
        private readonly Mock<IItemRepository> _repository;

        public ItemControllerShould()
        {
            _mockLogger = new Mock<ILogger<ItemsController>>();
            _repository = new Mock<IItemRepository>();
            _controller = new ItemsController(_repository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetItemReturnsItem()
        {
            string name = "a";
            _repository.Setup(m => m.ItemExists(name)).Returns(true);
            _repository.Setup(m => m.GetByNameAsync(name)).Returns(Task.FromResult(new Item { Name = "a" }));

            var okResult = await _controller.GetItem("a") as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Reviews.API.Controllers;
using Reviews.Data.Entities;
using Reviews.Data.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Reviews.Tests
{
    public class ReviewsControllerShould
    {
        private readonly Mock<ILogger<ReviewsController>> _mockLogger;
        private readonly ReviewsController _controller;
        private readonly Mock<IItemRepository> _repository;
        private readonly Mock<IMapper> _mapper;

        public ReviewsControllerShould()
        {
            _mockLogger = new Mock<ILogger<ReviewsController>>();
            _repository = new Mock<IItemRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new ReviewsController(_repository.Object, _mockLogger.Object, _mapper.Object);
        }

        [Fact]
        public async Task GetReviewsByNameReturnsReviews()
        {
            string name = "a";
            IEnumerable<Review> reviews = new List<Review>() { new Review { Rating = 9 } };

            _repository.Setup(m => m.ItemExists(name)).Returns(true);
            _repository.Setup(m => m.GetReviewsByNameAsync(name)).Returns(Task.FromResult(reviews));

            var okResult = await _controller.GetReviewsByItemName(name) as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetReviewsWithGreaterRatingReturnsReviews()
        {
            Item item = new Item
            {
                Reviews = new List<Review>
            { new Review { Rating = 8 }, new Review { Rating = 9 } }
            };

            IEnumerable<Item> items = new List<Item>() { item };

            _repository.Setup(m => m.GetByAverageReviewRatingGreaterThanAsync(4)).Returns(Task.FromResult(items));

            var okResult = await _controller.GetReviewByAverageRatingGreaterThan(4) as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task GetReviewsWithLowerRatingReturnsReviews()
        {
            Item item = new Item
            {
                Reviews = new List<Review>
            { new Review { Rating=2}, new Review { Rating= 1} }
            };

            IEnumerable<Item> items = new List<Item>() { item };

            _repository.Setup(m => m.GetByAverageReviewRatingLowerThanAsync(4)).Returns(Task.FromResult(items));

            var okResult = await _controller.GetReviewByAverageRatingLowerThan(4) as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);
        }
    }
}

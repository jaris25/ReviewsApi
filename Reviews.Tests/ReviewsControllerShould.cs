using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Reviews.API.Controllers;
using Reviews.Data.Contexts;
using Reviews.Data.Entities;
using Reviews.Data.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Reviews.Tests
{
    public class ReviewsControllerShould
    {
        private readonly Mock<ILogger<ReviewsController>> _mockLogger;
        private readonly Mock<IMapper> _mapper;

        public ReviewsControllerShould()
        {
            _mockLogger = new Mock<ILogger<ReviewsController>>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task GetReviewsByNameReturnsReviews()
        {
            var options = new DbContextOptionsBuilder<ReviewsContext>()
           .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
          .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item", Reviews = new List<Review>() { new Review { Feedback = "smth", Rating = 3 } } });
                context.SaveChanges();
            }

            using (var context = new ReviewsContext(options))
            {
                ItemRepository repo = new ItemRepository(context);
                var _controller = new ReviewsController(repo, _mockLogger.Object, _mapper.Object);
                var okResult = await _controller.GetReviewsByItemName("item") as OkObjectResult;
                Assert.Equal(200, okResult.StatusCode);
            }
        }

        [Fact]
        public async Task GetReviewsWithGreaterRatingReturnsReviews()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item", Reviews = new List<Review>() { new Review { Feedback = "smth", Rating = 3 } } });
                context.SaveChanges();
            }

            using (var context = new ReviewsContext(options))
            {
                ItemRepository repo = new ItemRepository(context);
                var _controller = new ReviewsController(repo, _mockLogger.Object, _mapper.Object);
                var okResult = await _controller.GetReviewByAverageRatingGreaterThan(2) as OkObjectResult;
                Assert.Equal(200, okResult.StatusCode);
            }
        }

        [Fact]
        public async Task GetReviewsWithLowerRatingReturnsReviews()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item", Reviews = new List<Review>() { new Review { Feedback = "smth", Rating = 3 } } });
                context.SaveChanges();
            }

            using (var context = new ReviewsContext(options))
            {
                ItemRepository repo = new ItemRepository(context);
                var _controller = new ReviewsController(repo, _mockLogger.Object, _mapper.Object);
                var okResult = await _controller.GetReviewByAverageRatingLowerThan(2) as OkObjectResult;
                Assert.Equal(200, okResult.StatusCode);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Reviews.Data.Contexts;
using Reviews.Data.Entities;
using Reviews.Data.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Reviews.Tests
{
    public class ItemRepositoryShould
    {
        [Fact]
        public async Task GetItemReviewsWithAverageGreaterThan()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                            .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                            .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item", Reviews = new List<Review>() { new Review { Feedback = "smth", Rating = 10 } } });
                context.SaveChanges();
                IItemRepository repo = new ItemRepository(context);

                var result = await repo.GetByAverageReviewRatingGreaterThanAsync(9.5);
                Assert.Single(result);
            }
        }
        [Fact]
        public async Task GetByNameReturnsItem()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                            .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                            .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item", Reviews = new List<Review>() { new Review { Feedback = "smth", Rating = 10 } } });
                context.SaveChanges();
                IItemRepository repo = new ItemRepository(context);
                var result = await repo.GetByNameAsync("item");
                Assert.Equal("item", result.Name);
            }
        }
        [Fact]
        public async Task GetReviewByNameReturnsItem()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                            .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                            .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item", Reviews = new List<Review>() { new Review { Feedback = "smth", Rating = 10 } } });
                context.SaveChanges();
                IItemRepository repo = new ItemRepository(context);
                var result = await repo.GetReviewsByItemNameAsync("item");
                Assert.Single(result);
            }
        }
        [Fact]
        public void ItemExsitsReturnsTrue()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                            .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                            .Options;

            using (var context = new ReviewsContext(options))
            {

                context.Items.Add(new Item { Name = "item", Reviews = new List<Review>() { new Review { Feedback = "smth", Rating = 10 } } });
                context.SaveChanges();
                IItemRepository repo = new ItemRepository(context);
                var result = repo.ItemExists("item");
                Assert.True(result);
            }
        }

        [Fact]
        public async Task CreatesReviewForItem()
        {

            var options = new DbContextOptionsBuilder<ReviewsContext>()
                            .UseInMemoryDatabase(databaseName: $"ItemsDatabase{Guid.NewGuid()}")
                            .Options;

            using (var context = new ReviewsContext(options))
            {
                var review = new Review { Feedback = "smth", Rating = 10 };
                context.Items.Add(new Item { Name = "item" });
                context.SaveChanges();
                IItemRepository repo = new ItemRepository(context);
                await repo.LeaveReviewAsync("item", review);
                var reviewFromItem = await repo.GetReviewsByItemNameAsync("item");
                Assert.Single(reviewFromItem);
            }
        }
    }
}

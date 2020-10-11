using Microsoft.EntityFrameworkCore;
using Reviews.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reviews.Data.Contexts
{
    public class ReviewsContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

        public ReviewsContext(DbContextOptions<ReviewsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                 .HasData(
                new Item()
                {
                    Id = 1,
                    Name = "Fork"
                },
                new Item()
                {
                    Id = 2,
                    Name = "Spoon"
                },
                new Item()
                {
                    Id = 3,
                    Name = "Apple"
                });


            modelBuilder.Entity<Review>()
              .HasData(
                new Review()
                {
                    Id = 1,
                    ItemId = 1,
                    Rating = 5,
                    Feedback = "Average spoon, could be better."

                },
                new Review()
                {
                    Id = 2,
                    ItemId = 3,
                    Rating = 1,
                    Feedback = "That's a spoiled apple fam."
                },
                new Review()
                {
                    Id = 3,
                    ItemId = 2,
                    Rating = 10,
                    Feedback = "That's a brilliant fork."
                },
                new Review()
                {
                    Id = 4,
                    ItemId = 2,
                    Rating = 8,
                    Feedback = "Very good fork."
                },
                new Review()
                {
                    Id = 5,
                    ItemId = 3,
                    Rating = 2,
                    Feedback = "U call this food????"
                },
                new Review()
                {
                    Id = 6,
                    ItemId = 2,
                    Rating = 9,
                    Feedback = "That's one of the best forks!!!"
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

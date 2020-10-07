﻿using Reviews.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reviews.Data.Services
{
    public interface IItemRepository
    {
        Task<Item> GetByNameAsync(string name);
        Task<IEnumerable<Item>> GetByAverageReviewRatingGreaterThanAsync(double reviewRating);
        Task<IEnumerable<Item>> GetByAverageReviewRatingLowerThanAsync(double reviewRating);
        Task<IEnumerable<Review>> GetReviewsByNameAsync(string name);
        Task LeaveReviewAsync(string name, Review review);
        bool ItemExists(string name);
    }
}

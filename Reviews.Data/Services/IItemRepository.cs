using Reviews.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reviews.Data.Services
{
    public interface IItemRepository
    {
        Task<Item> GetByNameAsync(string name);
        Task<IEnumerable<Item>> GetByReviewRatingAsync(int reviewRating);
        Task<IEnumerable<Item>> GetByAverageReviewRatingAsync(double reviewRating);
        Task<IEnumerable<Review>> GetReviewsByNameAsync(string name);
        Task LeaveReviewAsync(int itemId, Review review);
        bool ItemExists(string name);
    }
}

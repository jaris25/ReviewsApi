using Microsoft.EntityFrameworkCore;
using Reviews.Data.Contexts;
using Reviews.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Data.Services
{
    public class ItemRepository : IItemRepository
    {

        private readonly ReviewsContext _context;

        public ItemRepository(ReviewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetByAverageReviewRatingAsync(double reviewRating)
        {
            var items = await _context.Items.Select(i => i).Where(i => i.Reviews.Average(r => r.Rating) >= reviewRating).ToListAsync();
            return items;
        }

        public Task<Item> GetByNameAsync(string name)
        {
            var result = _context.Items.Where(i => i.Name.ToLower() == name).FirstOrDefault();
            return Task.FromResult(result);
        }

        public async Task<IEnumerable<Item>> GetByReviewRatingAsync(int reviewRating)
        {
            return await _context.Reviews.Where(r => r.Rating >= reviewRating).Select(r => r.Item).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByIdAsync(int itemId)
        {
            return await _context.Reviews.Where(r => r.ItemId == itemId).Select(r => r).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByNameAsync(string name)
        {
            return await _context.Reviews.Where(r => r.Item.Name.ToLower() == name).ToListAsync();
        }

        public bool ItemExists(string name)
        {
            return _context.Items.Any(i => i.Name.ToLower() == name);
        }

        public async Task LeaveReviewAsync(int itemId, Review review)
        {
            var item = _context.Items.Where(i => i.Id == itemId).FirstOrDefault();
            await Task.Run(() => item.Reviews.Add(review));
        }
    }
}

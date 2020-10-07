using Microsoft.EntityFrameworkCore;
using Reviews.Data.Contexts;
using Reviews.Data.Entities;
using Reviews.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<IEnumerable<Item>> GetByAverageReviewRatingGreaterThanAsync(double reviewRating)
        {
            var items = await _context.Items.Include(i => i.Reviews)
                .Where(i => i.Reviews.Average(r => r.Rating) >= reviewRating).ToListAsync();
            return items;
        }

        public async Task<IEnumerable<Item>> GetByAverageReviewRatingLowerThanAsync(double reviewRating)
        {
            var items = await _context.Items.Include(i => i.Reviews)
                .Where(i => i.Reviews.Average(r => r.Rating) <= reviewRating).ToListAsync();
            return items;
        }

        public async Task<Item> GetByNameAsync(string name)
        {
            var result = _context.Items.Include(i => i.Reviews).Where(i => i.Name.ToLower() == name).FirstOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Review>> GetReviewsByNameAsync(string name)
        {
            return await _context.Reviews.Where(r => r.Item.Name.ToLower() == name).ToListAsync();
        }

        public bool ItemExists(string name)
        {
            return _context.Items.Any(i => i.Name.ToLower() == name);
        }

        public async Task LeaveReviewAsync(string name, Review review)
        {
            var item = _context.Items.Where(i => i.Name.ToLower() == name.ToLower()).FirstOrDefault();
            await Task.Run(() => item.Reviews.Add(review));
            _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PieShopAdmin.Database;

namespace PieShopAdmin.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PieShopDbContext _pieShopDbContext;

        public CategoryRepository(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _pieShopDbContext.Categories.OrderBy(p => p.CategoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _pieShopDbContext.Categories.OrderBy(p => p.CategoryId).ToListAsync();
        }

        public async Task<Category>? GetCategoryByIdAsync(int id)
        {
            return await _pieShopDbContext.Categories.Include(p => p.Pies).FirstOrDefaultAsync(c => c.CategoryId == id);
        }
    }
}

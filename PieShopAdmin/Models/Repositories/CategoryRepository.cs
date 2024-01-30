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

        public async Task<int> AddCategoryAsync(Category category)
        {
            bool isCategoryWithSameNameExist = await _pieShopDbContext.Categories.AnyAsync(c => c.Name == category.Name);
            if (isCategoryWithSameNameExist) 
            {
                throw new Exception("A category with the same name already exist");
            }

            _pieShopDbContext.Categories.Add(category);
            return await _pieShopDbContext.SaveChangesAsync();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _pieShopDbContext.Categories.OrderBy(p => p.CategoryId);
        }

        //when we read lists we dont need to truck changes
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _pieShopDbContext.Categories.OrderBy(p => p.CategoryId).AsNoTracking().ToListAsync();
        }


        //when we read lists we dont need to truck changes
        public async Task<Category>? GetCategoryByIdAsync(int id)
        {
            return await _pieShopDbContext.Categories.Include(p => p.Pies).AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }
    }
}

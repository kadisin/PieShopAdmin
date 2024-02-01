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

        public async Task<int> DeleteCategoryAsync(int id)
        {
            var categoryToDelete = await _pieShopDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);

            var piesInCategory = _pieShopDbContext.Pies.Any(x => x.CategoryId == id);
            
            if(piesInCategory)
            {
                throw new Exception("Pies exist in this category. Delete all pies in this category before deleting the category");
            }
            
            if (categoryToDelete != null)
            {
                _pieShopDbContext.Categories.Remove(categoryToDelete);
                return await _pieShopDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The category to delete can't be found");
            }
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

        public async Task<int> UpdateCategoryAsync(Category category)
        {
            bool isCategoryWithSameNameExist = await _pieShopDbContext.
                Categories.AnyAsync(c => c.Name == category.Name && c.CategoryId != category.CategoryId);
            if (isCategoryWithSameNameExist)
            {
                throw new Exception("A category with the same name already exists");
            }

            var categoryToUpdate = await _pieShopDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == category.CategoryId);
            if (categoryToUpdate != null) 
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;

                _pieShopDbContext.Categories.Update(categoryToUpdate);
                return await _pieShopDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"The category to update can't be found");
            }
        }
    }
}

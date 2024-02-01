using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PieShopAdmin.Database;

namespace PieShopAdmin.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PieShopDbContext _pieShopDbContext;
        private IMemoryCache _memoryCache;

        public CategoryRepository(PieShopDbContext pieShopDbContext, IMemoryCache memoryCache)
        {
            _pieShopDbContext = pieShopDbContext;
            _memoryCache = memoryCache;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            bool isCategoryWithSameNameExist = await _pieShopDbContext.Categories.AnyAsync(c => c.Name == category.Name);
            if (isCategoryWithSameNameExist) 
            {
                throw new Exception("A category with the same name already exist");
            }

            _pieShopDbContext.Categories.Add(category);
            int result = await _pieShopDbContext.SaveChangesAsync();

            //When we add category we have to remove categories cache - it is not up-to-date
            _memoryCache.Remove(Consts.AllCategoriesCacheName);
            return result;
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
                int result = await _pieShopDbContext.SaveChangesAsync();

                //When we delete category we have to remove categories cache - it is not up-to-date
                _memoryCache.Remove(Consts.AllCategoriesCacheName);
                return result;
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
            List<Category> allCategories = null;
            if (!_memoryCache.TryGetValue(Consts.AllCategoriesCacheName, out allCategories))
            {
                //if all categories not exist in memory then add it to cache
                allCategories = await _pieShopDbContext.Categories.AsNoTracking().OrderBy(c => c.CategoryId).ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(60));
                _memoryCache.Set(Consts.AllCategoriesCacheName, allCategories, cacheEntryOptions);
            }
            return allCategories;
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
                int result = await _pieShopDbContext.SaveChangesAsync();

                //When we modify category we have to remove categories cache - it is not up-to-date
                _memoryCache.Remove(Consts.AllCategoriesCacheName);
                return result;
            }
            else
            {
                throw new ArgumentException($"The category to update can't be found");
            }
        }
    }
}

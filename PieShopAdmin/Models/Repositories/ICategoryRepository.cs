namespace PieShopAdmin.Models.Repositories
{
    /// <summary>
    /// ICategoryRepository define operations related to Category on database
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        IEnumerable<Category> GetAllCategories();

        /// <summary>
        /// Get all categories async method
        /// </summary>
        /// <returns>List of categories</returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        /// <summary>
        /// Get category using id
        /// </summary>
        /// <param name="id">Id category</param>
        /// <returns>Category object</returns>
        Task<Category>? GetCategoryByIdAsync(int id);

        /// <summary>
        /// Add category to database
        /// </summary>
        /// <param name="category">Object to add to database</param>
        /// <returns></returns>
        Task<int> AddCategoryAsync(Category category);

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="category">Category to update</param>
        /// <returns></returns>
        Task<int> UpdateCategoryAsync(Category category);

        /// <summary>
        /// Remove category from database
        /// </summary>
        /// <param name="id">Id of category to be deleted</param>
        /// <returns></returns>
        Task<int> DeleteCategoryAsync(int id);
    }
}

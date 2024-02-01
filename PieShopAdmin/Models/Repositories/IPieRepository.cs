namespace PieShopAdmin.Models.Repositories
{
    /// <summary>
    /// IPieRepository define methods related to Pie on database
    /// </summary>
    public interface IPieRepository
    {
        /// <summary>
        /// Method to get all pies
        /// </summary>
        /// <returns>List of pies</returns>
        Task<IEnumerable<Pie>> GetAllPiesAsync();

        /// <summary>
        /// Method to get pie using id
        /// </summary>
        /// <param name="id">Id of pie</param>
        /// <returns>Pie with id</returns>
        Task<Pie>? GetPieByIdAsync(int id);

        /// <summary>
        /// Add pie to database
        /// </summary>
        /// <param name="pie">Object pie to add to database</param>
        /// <returns></returns>
        Task<int> AddPieAsync(Pie pie);
        /// <summary>
        /// Update existing pie in database
        /// </summary>
        /// <param name="pie">Pie to be updated</param>
        /// <returns></returns>
        Task<int> UpdatePieAsync(Pie pie);

        /// <summary>
        /// Delete pie from database
        /// </summary>
        /// <param name="id">Id related to pie to be deleted</param>
        /// <returns></returns>
        Task<int> DeletePieAsync(int id);

        /// <summary>
        /// Get amount of pie in database
        /// </summary>
        /// <returns>Number of pies in database</returns>
        Task<int> GetAllPiesCountAsync();

        /// <summary>
        /// Get part of pies from database
        /// </summary>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Amount pies in page</param>
        /// <returns>List of pies (part of)</returns>
        Task<IEnumerable<Pie>> GetPiesPagedAsync(int? pageNumber, int pageSize);

        /// <summary>
        /// Get part of pies from database
        /// </summary>
        /// <param name="sortBy">Sorting parameter</param>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Amount pies in page</param>
        /// <returns>List of sorted pies (part of)</returns>
        Task<IEnumerable<Pie>> GetPiesSortedAndPagedAsync(string sortBy, int? pageNumber, int pageSize);

        /// <summary>
        /// Filtering pies by searchQuery
        /// </summary>
        /// <param name="searchQuery">Paramter use to filter pies contains that word (in name, short desciption, long description, no matter about big/small letters)</param>
        /// <param name="categoryid">Parameter use to get only pies in defined category - using id</param>
        /// <returns>List of pie found related to two filters - searchQuery and Category</returns>
        Task<IEnumerable<Pie>> SearchPies(string searchQuery, int? categoryid);
    }
}

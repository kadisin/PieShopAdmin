namespace PieShopAdmin.Models.Repositories
{
    public interface IPieRepository
    {
        Task<IEnumerable<Pie>> GetAllPiesAsync();
        Task<Pie>? GetPieByIdAsync(int id);
        Task<int> AddPieAsync(Pie pie);
        Task<int> UpdatePieAsync(Pie pie);
        Task<int> DeletePieAsync(int id);
        Task<int> GetAllPiesCountAsync();
        Task<IEnumerable<Pie>> GetPiesPagedAsync(int? pageNumber, int pageSize);
        Task<IEnumerable<Pie>> GetPiesSortedAndPagedAsync(string sortBy, int? pageNumber, int pageSize);
        Task<IEnumerable<Pie>> SearchPies(string searchQuery, int? categoryid);
    }
}

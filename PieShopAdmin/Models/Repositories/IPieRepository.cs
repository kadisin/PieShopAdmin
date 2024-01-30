namespace PieShopAdmin.Models.Repositories
{
    public interface IPieRepository
    {
        Task<IEnumerable<Pie>> GetAllPiesAsync();
        Task<Pie>? GetPieByIdAsync(int id);
        Task<int> AddPieAsync(Pie pie);
    }
}

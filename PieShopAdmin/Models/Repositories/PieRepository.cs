using Microsoft.EntityFrameworkCore;
using PieShopAdmin.Database;

namespace PieShopAdmin.Models.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly PieShopDbContext _pieShopDbContext;

        public PieRepository(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }

        public async Task<IEnumerable<Pie>> GetAllPiesAsync()
        {
            return await _pieShopDbContext.Pies.OrderBy(x => x.PieId).ToListAsync();
        }

        public async Task<Pie>? GetPieByIdAsync(int id)
        {
            return await _pieShopDbContext.Pies.Include(x => x.Ingredients).Include(p => p.Category).FirstOrDefaultAsync(z => z.PieId == id);
        }
    }
}

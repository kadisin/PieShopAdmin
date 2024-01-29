using Microsoft.EntityFrameworkCore;
using PieShopAdmin.Database;

namespace PieShopAdmin.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly PieShopDbContext _pieShopDbContext;

        public OrderRepository(PieShopDbContext pieShopDbContext)
        {
            _pieShopDbContext = pieShopDbContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync()
        {
            return await _pieShopDbContext.Orders.Include(p => p.OrderDetails).ThenInclude(o => o.Pie).OrderBy(x => x.OrderId).ToListAsync();
        }

        public async Task<Order>? GetOrderDetailsAsync(int? orderId)
        {
            if(orderId != null)
            {
                var order = await _pieShopDbContext.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Pie).OrderBy(x => x.OrderId)
                    .Where(z => z.OrderId == orderId.Value).FirstOrDefaultAsync();
                return order;
            }
            return null;
        }
    }
}

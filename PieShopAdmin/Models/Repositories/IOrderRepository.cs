namespace PieShopAdmin.Models.Repositories
{
    /// <summary>
    /// IOrderRepository define operations related to Order on database
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Get order details by id
        /// </summary>
        /// <param name="orderId">Id related to order</param>
        /// <returns>Object order</returns>
        Task<Order>? GetOrderDetailsAsync(int? orderId);

        /// <summary>
        /// Get all Orders with details
        /// </summary>
        /// <returns>List of orders</returns>
        Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync();
    }
}

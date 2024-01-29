using Microsoft.AspNetCore.Mvc;
using PieShopAdmin.Models;
using PieShopAdmin.Models.Repositories;
using PieShopAdmin.ViewModel;

namespace PieShopAdmin.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index(int? orderId, int? orderDetailId)
        {
            OrderIndexViewModel model = new()
            {
                Orders = await _orderRepository.GetAllOrdersWithDetailsAsync()
            };

            if(orderId != null)
            {
                Order selectedOrder = model.Orders.Where(o => o.OrderId == orderId).Single();
                model.OrderDetails = selectedOrder.OrderDetails;
                model.SelectedOrderId = orderId;
            }

            if(orderDetailId != null)
            {
                var selectedOrderDetail = model.OrderDetails.Where(o => o.OrderDetailId == orderDetailId).Single();
                model.Pies = new List<Pie>() { selectedOrderDetail.Pie };
                model.SelectedOrderDetailId = orderDetailId;
            }
            return View(model);
        }
    }
}

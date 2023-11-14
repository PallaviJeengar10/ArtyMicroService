using SharedModels.Models;
using UserInteraction.DataRepositories.Orders;

namespace UserInteraction.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<int> DeleteOrderProduct(int orderId, int productId)
        {
            OrderDetail orderDetail = await _orderRepository.GetOrderDetail(orderId, productId);
            return await _orderRepository.DeleteOrderProduct(orderDetail);
        }

        public async Task<List<OrderDetail>> GetOrderDetailsById(int orderId)
        {
            return await _orderRepository.GetOrderDetailsById(orderId);
        }

        public Task<List<Order>> GetOrderList()
        {
            return _orderRepository.GetOrderList();
        }

        public async Task<bool> UpdateOrder(int orderId, Order updatedOrder)
        {
            Order order = await _orderRepository.GetOrderById(orderId);
            if (order != null)
            {
                await _orderRepository.UpdateOrder(order, updatedOrder);
                return true;
            }
            return false;
        }
    }
}

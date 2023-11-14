using SharedModels.Models;

namespace UserInteraction.Services.Orders
{
    public interface IOrderService
    {
        public Task<List<Order>> GetOrderList();
        public Task<List<OrderDetail>> GetOrderDetailsById(int orderId);
        public Task<int> DeleteOrderProduct(int orderId, int productId);
        public Task<bool> UpdateOrder(int orderId, Order order);
    }
}

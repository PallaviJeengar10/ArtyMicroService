using SharedModels.Models;

namespace UserInteraction.DataRepositories.Orders
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrderList();
        public Task<List<OrderDetail>> GetOrderDetailsById(int orderId);
        public Task<int> DeleteOrderProduct(OrderDetail order);
        public Task UpdateOrder(Order order, Order updatedOrder);
        public Task<OrderDetail> GetOrderDetail(int orderId, int productId);
        public Task<Order> GetOrderById(int orderId);
    }
}

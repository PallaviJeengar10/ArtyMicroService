using Arty.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

namespace UserInteraction.DataRepositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ArtyContext _context;
        public OrderRepository(ArtyContext context)
        {
            _context = context;
        }
        public async Task<int> DeleteOrderProduct(OrderDetail order)
        {
            _context.OrderDetails.Remove(order);
            await _context.SaveChangesAsync();
            return order.OrderDetailId;
        }

        public async Task<OrderDetail> GetOrderDetail(int orderId, int productId)
        {
           var orderDetails = await _context.OrderDetails
                    .FirstOrDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            return orderDetails ?? new OrderDetail();
        }

        public async Task<List<OrderDetail>> GetOrderDetailsById(int orderId)
        {
            return await _context.OrderDetails
                    .Where(o => o.OrderId == orderId)
                    .ToListAsync();
        }

        public async Task<List<Order>> GetOrderList()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task UpdateOrder(Order order, Order updatedOrder)
        {
            if (updatedOrder.OrderDate != DateTime.MinValue)
            {
                order.OrderDate = updatedOrder.OrderDate;
            }
            order.OrderStatus = updatedOrder.OrderStatus ?? order.OrderStatus;

            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int OrderId)
        {
            var order = await _context.Orders.FindAsync(OrderId);
            return order ?? new Order();
        }
    }
}

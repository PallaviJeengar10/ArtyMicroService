using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using UserInteraction.Services.Orders;

namespace Arty.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("getOrderList")]
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetOrderList()
        {
            List<Order> orders = await _orderService.GetOrderList();
            return Ok(orders);
        }

        [Route("getOrderDetails/{orderId}")]
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            List<OrderDetail> orderdetails = await _orderService.GetOrderDetailsById(orderId);
            return Ok(orderdetails);
        }

        [Route("removeOrderProduct/{orderId}/{productId}")]
        [HttpDelete]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteOrderProduct(int orderId, int productId)
        {
            int deletedId = await _orderService.DeleteOrderProduct(orderId, productId);
            return Ok(deletedId);
        }

        [Route("updateOrder/{orderId}")]
        [HttpPut]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> UpdateOrder(int orderId, Order order)
        {
            if (orderId == 0)
            {
                return await Task.FromResult<IActionResult>(NotFound());
            }
            if (await _orderService.UpdateOrder(orderId, order))
            {
                return Ok("Order Updated");
            }
            return NotFound();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaypalCheckoutExample.Clients;
using System.Security.Claims;

namespace PaypalCheckoutExample.Controllers
{
    [Authorize]
    public class PaypalController : Controller
    {
        private readonly PaypalClient _paypalClient;

        public PaypalController(PaypalClient paypalClient)
        {
            this._paypalClient = paypalClient;
        }

        //[HttpPost]
        //public IActionResult Index(string orderData)
        //{
        //    PlaceOrder placeOrder = JsonConvert.DeserializeObject<PlaceOrder>(orderData);

        //    decimal totalAmount = placeOrder.orderDetails.Sum(od => od.Price * od.Quantity);
        //    ViewBag.ClientId = _paypalClient.ClientId;
        //    ViewBag.Price = totalAmount.ToString();
        //    ViewBag.PlaceOrder = placeOrder;

        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Order([FromBody] OrderModel orderModel)
        //{
        //    try
        //    {
        //        var currency = "USD";
        //        var reference = "INV001";
        //        var response = await _paypalClient.CreateOrder(orderModel.Price.ToString(), currency, reference);

        //        return Ok(response);
        //    }
        //    catch (Exception e)
        //    {
        //        var error = new
        //        {
        //            e.GetBaseException().Message
        //        };

        //        return BadRequest(error);
        //    }
        //}

        public async Task<IActionResult> Capture(string orderId)
        {
            try
            {
                var response = await _paypalClient.CaptureOrder(orderId);
                var reference = response.purchase_units[0].reference_id;
                return Ok(response);
            }
            catch (Exception e)
            {
                var error = new
                {
                    e.GetBaseException().Message
                };

                return BadRequest(error);
            }
        }

        //public async Task<IActionResult> Success([FromBody] PlaceOrder placeOrder)
        //{
        //    try
        //    {
        //        //var userIdValue = HttpContext.User.FindFirstValue("UserId");
        //        //if (int.TryParse(userIdValue, out var userId))
        //        //{
        //        //    Order orderObject = placeOrder.Order;
        //        //    orderObject.UserId = userId;

        //        //    decimal totalAmount = placeOrder.orderDetails.Sum(od => od.Price * od.Quantity);
        //        //    int orderId = await SQLOrder.Add(placeOrder.Order);
        //        //    foreach (OrderDetail orderDetail in placeOrder.orderDetails)
        //        //    {
        //        //        orderDetail.OrderId = orderId;
        //        //        await SQLOrderDetail.Add(orderDetail);
        //        //    }

        //        //    int cartId = await SQLCartItem.GetCartIdByUser(userId);
        //        //    await SQLCartItem.DeleteAllCartItems(cartId);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occurred: " + ex.Message);
        //    }

        //    return RedirectToAction("ListOrder", "Order");
        //}
   
    }
}

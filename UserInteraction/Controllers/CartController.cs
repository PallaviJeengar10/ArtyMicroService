using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using UserInteraction.Services.Carts;

namespace Arty.Controllers
{
    [Route("cart")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;    
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Route("viewCartItem")]
        [HttpGet]
        public async Task<IActionResult> ViewCart([FromQuery] int userId)
        {
            try
            {
                Cart cart = await _cartService.GetCart(userId);

                if (cart == null)
                {
                    return NotFound("Cart details not found.");
                }

                List<CartItem> cartItems = await _cartService.GetCartItem(cart.CartId);

                if (cartItems.Count == 0)
                {
                    return NotFound("Cart is Emplty.");
                }

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
    }
}

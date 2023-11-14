using SharedModels.Models;

namespace UserInteraction.Services.Carts
{
    public interface ICartService
    {
        public Task<Cart> GetCart(int userId);
        public Task<List<CartItem>> GetCartItem(int cartId);
    }
}

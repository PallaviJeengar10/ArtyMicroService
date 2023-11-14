using SharedModels.Models;

namespace UserInteraction.DataRepositories.Carts
{
    public interface ICartRepository
    {
        public Task<Cart> GetCart(int userId);
        public Task<List<CartItem>> GetCartItem(int cartId);
        public Task<bool> CreateCart(int UserId);
    }
}

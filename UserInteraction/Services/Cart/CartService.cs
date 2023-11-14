using SharedModels.Models;
using UserInteraction.DataRepositories.Carts;

namespace UserInteraction.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }
        public async Task<Cart> GetCart(int userId)
        {
            return await _repository.GetCart(userId);
        }

        public async Task<List<CartItem>> GetCartItem(int cartId)
        {
           return await GetCartItem(cartId);
        }
    }
}

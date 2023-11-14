using Arty.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

namespace UserInteraction.DataRepositories.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly ArtyContext _context;
        public CartRepository(ArtyContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCart(int UserId)
        {
            try
            {
                await _context.Carts.AddAsync(new Cart
                {
                    UserId = UserId
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }                   
        }

        public async Task<Cart> GetCart(int userId)
        {
            var cart = await _context.Carts
                     .FirstOrDefaultAsync(c => c.UserId == userId);
            return cart ?? new Cart();
        }

        public async Task<List<CartItem>> GetCartItem(int cartId)
        {
            return await _context.CartItems
                    .Where(c => c.CartId == cartId)
                    .ToListAsync();
        }

    }
}

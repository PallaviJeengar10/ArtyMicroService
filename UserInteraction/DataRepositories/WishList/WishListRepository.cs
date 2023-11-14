using Arty.Models;
using Microsoft.EntityFrameworkCore;
using SharedModels.Models;

namespace UserInteraction.DataRepositories.WishLists
{
    public class WishListRepository : IWishListRepository
    {
        private readonly ArtyContext _context;
        public WishListRepository(ArtyContext context)
        {
            _context = context;
        }

        public async Task<int> AddToWishList(WishListItem wishListItem)
        {
            await _context.WishListItems.AddAsync(wishListItem);
            await _context.SaveChangesAsync();
            return wishListItem.WishListItemId;
        }

        public async Task<bool> CreateWishList(int UserId)
        {
            try
            {
                await _context.WishLists.AddAsync(new WishList
                {
                    UserId = UserId
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task DeleteWishListItem(WishListItem wishListItem)
        {
            _context.WishListItems.Remove(wishListItem);
            await _context.SaveChangesAsync();
        }

        public async Task<WishList> GetWishList(int userId)
        {
            var wishList = await _context.WishLists
                     .FirstOrDefaultAsync(c => c.UserId == userId);
            return wishList ?? new WishList();
        }

        public async Task<WishListItem> GetWishListItemById(int wishListId, int productId)
        {
            var wishListItem = await _context.WishListItems
                .FirstOrDefaultAsync(w => w.WishListId == wishListId && w.ProductId == productId);
            return wishListItem ?? new WishListItem();
        }

        public async Task<List<WishListItem>> GetWishListItems(int wishListId)
        {
            return await _context.WishListItems
                    .Where(c => c.WishListId == wishListId)
                    .ToListAsync();
        }
    }
}

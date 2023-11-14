using SharedModels.Models;

namespace UserInteraction.Services.WishLists
{
    public interface IWishListService
    {
        public Task<WishList> GetWishList(int userId);
        public Task<List<WishListItem>> GetWishListItems(int wishListId);
        public Task<int> AddWishList(WishList wishList, int productId);
        public Task<WishListItem> GetWishListItemById(int wishListId, int productId);
        public Task DeleteWishListItem(WishListItem wishListItem);
    }
}

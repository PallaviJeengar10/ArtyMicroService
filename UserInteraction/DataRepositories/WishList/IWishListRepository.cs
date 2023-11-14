using SharedModels.Models;

namespace UserInteraction.DataRepositories.WishLists
{
    public interface IWishListRepository
    {
        public Task<WishList> GetWishList(int userId);
        public Task<List<WishListItem>> GetWishListItems(int wishListId);
        public Task<int> AddToWishList(WishListItem wishListItem);
        public Task<WishListItem> GetWishListItemById(int wishListId, int productId);
        public Task DeleteWishListItem(WishListItem wishListItem);
        public Task<bool> CreateWishList(int UserId);
    }
}

using AutoMapper;
using SharedModels.Models;
using UserInteraction.DataRepositories.WishLists;

namespace UserInteraction.Services.WishLists
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _repository;
        private readonly IMapper _mapper;

        public WishListService(IWishListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddWishList(WishList wishList, int productId)
        {
            var wishListItem = _mapper.Map<WishListItem>(wishList);
            wishListItem.ProductId = productId;
            return await _repository.AddToWishList(wishListItem);
        }

        public async Task DeleteWishListItem(WishListItem wishListItem)
        {
            await _repository.DeleteWishListItem(wishListItem);
        }

        public async Task<WishList> GetWishList(int userId)
        {
            return await _repository.GetWishList(userId);
        }

        public async Task<WishListItem> GetWishListItemById(int wishListId, int productId)
        {
            return await _repository.GetWishListItemById(wishListId, productId);
        }

        public async Task<List<WishListItem>> GetWishListItems(int wishListId)
        {
            return await _repository.GetWishListItems(wishListId);
        }
    }
}

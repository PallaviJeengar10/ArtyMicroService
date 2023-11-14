using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedModels.Models;
using UserInteraction.Services.WishLists;

namespace Arty.Controllers
{
    [Route("wishList")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;
        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }

        [Route("viewWishListItem")]
        [HttpGet]
        public async Task<IActionResult> ViewWishList([FromQuery] int userId)
        {
            try
            {
                WishList wishList = await _wishListService.GetWishList(userId);

                if (wishList == null)
                {
                    return NotFound("WishList details not found.");
                }

                List<WishListItem> wishListItems = await _wishListService.GetWishListItems(wishList.WishListId);

                if (wishListItems.Count == 0)
                {
                    return NotFound("WishList is Emplty.");
                }

                return Ok(wishListItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        [Route("deleteWishListItem")]
        [HttpDelete]
        public async Task<IActionResult> DeleteWishListItem(
                [FromQuery] int userId,
                [FromQuery] int productId)
        {
            WishList wishList = await _wishListService.GetWishList(userId);
            if (wishList == null)
            {
                return NotFound("WishList not found for user");
            }

            WishListItem wishListItem = await _wishListService.GetWishListItemById(wishList.WishListId,productId);

            if (wishListItem == null)
            {
                return NotFound("Product not found in Wish List.");
            }

            await _wishListService.DeleteWishListItem(wishListItem);

            return NoContent();
        }

        [Route("addToWishList")]
        [HttpPost]
        public async Task<IActionResult> AddToWishList([FromQuery] int userId, [FromQuery] int productId)
        {
            WishList wishList = await _wishListService.GetWishList(userId);
            if(wishList == null)
            {
                return NotFound("WishList not found for user");
            }
            int wishListItemId = await _wishListService.AddWishList(wishList, productId);
            return Ok(wishListItemId);
        }
    }
}

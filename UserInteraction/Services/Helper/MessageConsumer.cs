using MassTransit;
using SharedModels;
using SharedModels.Models;
using UserInteraction.DataRepositories.Carts;
using UserInteraction.DataRepositories.WishLists;

namespace UserInteraction.Services.Helper
{
    public class MessageConsumer : IConsumer<MessageModel>
    {
        private readonly ICartRepository _cartRepo;
        private readonly IWishListRepository _wishlistRepo;

        public MessageConsumer(ICartRepository cartRepo, IWishListRepository wishlistRepo)
        {
            _cartRepo = cartRepo;
            _wishlistRepo = wishlistRepo;
        }

        public async Task Consume(ConsumeContext<MessageModel> context)
        {
            switch (context.Message.Event)
            {
                case MessageEvent.CreateCart:
                    await _cartRepo.CreateCart(int.Parse(context.Message.Value));
                    break;

                case MessageEvent.CreateWishList:
                    await _wishlistRepo.CreateWishList(int.Parse(context.Message.Value));
                    break;
            }
        }
    }
}

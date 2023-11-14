using Arty.Dtos;
using SharedModels.Models;
using AutoMapper;

namespace Arty.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<WishList, WishListItem>().ReverseMap();
            CreateMap<Cart, CartItem>().ReverseMap();
        }
    }
}

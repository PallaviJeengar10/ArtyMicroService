using Arty.Dtos;
using Arty.Models;
using AutoMapper;
using SharedModels.Models;

namespace Arty.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
            CreateMap<Product, ProductsDto>().ReverseMap();
        }
    }
}

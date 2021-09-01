using AutoMapper;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Web.Models;

namespace PK.MmtShop.Web.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDto, ProductModel>().ReverseMap();
        }
    }
}

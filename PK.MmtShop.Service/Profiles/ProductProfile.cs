using AutoMapper;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Service.Entities;

namespace PK.MmtShop.Service.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, map => map.MapFrom(target => target.Id))
                .ForMember(dest => dest.CategoryId, map => map.MapFrom(target => target.CategoryId))
                .ForMember(dest => dest.Name, map => map.MapFrom(target => target.Name))
                .ForMember(dest => dest.Description, map => map.MapFrom(target => target.Description))
                .ForMember(dest => dest.Price, map => map.MapFrom(target => target.Price))
                .ForMember(dest => dest.Sku, map => map.MapFrom(target => target.Sku));

            CreateMap<ProductDto, Product>().ReverseMap();
        }
    }
}

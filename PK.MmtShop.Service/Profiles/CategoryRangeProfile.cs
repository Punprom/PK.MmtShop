using AutoMapper;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Service.Entities;

namespace PK.MmtShop.Service.Profiles
{
    public class CategoryRangeProfile : Profile
    {
        public CategoryRangeProfile()
        {
            CreateMap<CategoryRange, CategoryRangeDto>().ReverseMap();
        }
    }
}

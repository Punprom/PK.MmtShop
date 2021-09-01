using AutoMapper;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Service.Entities;

namespace PK.MmtShop.Service.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}

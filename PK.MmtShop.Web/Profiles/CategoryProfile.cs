using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PK.MmtShop.Domain.Dtos;
using PK.MmtShop.Web.Models;

namespace PK.MmtShop.Web.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, CategoryModel>().ReverseMap();
        }
    }
}

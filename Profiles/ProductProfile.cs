using AutoMapper;
using ShopBridge.Dtos;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Profiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>()
                .ForMember(n => n.ProductStatus, opt => opt.MapFrom(o => (o.ProductQuantity > 0) ? "In Stock" : "Out of Stock"));
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}

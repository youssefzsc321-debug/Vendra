using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;

namespace Vendra.Core.Helpers.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, GetProductlDto>()
                .ForMember(dest=>dest.BrandName,opt=>opt.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dest=>dest.TypeName,opt=>opt.MapFrom(src=>src.ProductType.Name))
                .ForMember(dest =>dest.Image, opt => opt.MapFrom<ProductUrlResolver>());

            CreateMap<ProductBrand, GetBrands>();
            CreateMap<ProductType, GetTypes>();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}

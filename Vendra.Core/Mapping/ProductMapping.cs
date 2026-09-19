using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;

namespace Vendra.Core.Mapping
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, GetAllDto>()
                .ForMember(dest=>dest.BrandName,opt=>opt.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dest=>dest.TypeName,opt=>opt.MapFrom(src=>src.ProductType.Name));
        }
    }
}

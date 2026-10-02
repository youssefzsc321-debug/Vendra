using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;

namespace Vendra.Infrastructure.Specifications.ProductSpec
{
    public class ProductWiteFiltersForCountSpec:BaseSpecification<Product>
    {
        public ProductWiteFiltersForCountSpec(ProductSpecParam specParams) :
             base
            (
                p => ((string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
                !specParams.BrandId.HasValue || specParams.BrandId == p.ProductBrandId) &&
             (!specParams.TypeId.HasValue || specParams.TypeId == p.ProductTypeId)

            )
        {
            
        }
    }
}

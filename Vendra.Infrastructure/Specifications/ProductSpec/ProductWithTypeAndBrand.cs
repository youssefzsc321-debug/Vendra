using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Dtos.Products;
using Vendra.Core.Enities;

namespace Vendra.Infrastructure.Specifications.ProductSpec
{
    public class ProductWithTypeAndBrand : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrand(ProductSpecParam specParams) :
            base
            (
                p => ((string.IsNullOrEmpty(specParams.Search)||p.Name.ToLower().Contains(specParams.Search))&&
                !specParams.BrandId.HasValue || specParams.BrandId == p.ProductBrandId) &&
             (!specParams.TypeId.HasValue || specParams.TypeId == p.ProductTypeId)
                
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            int skip = ((specParams.PageIndex - 1) * specParams.Take);
            ApplayPagination(skip, specParams.Take);
            if (specParams.Sort != null)
            {
                switch (specParams.Sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }



            }
            else
                AddOrderBy(p => p.Name);
        }

        public ProductWithTypeAndBrand(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

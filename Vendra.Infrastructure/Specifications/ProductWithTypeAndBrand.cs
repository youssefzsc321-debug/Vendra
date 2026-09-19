using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;

namespace Vendra.Infrastructure.Specifications
{
    public class ProductWithTypeAndBrand:BaseSpecification<Product>
    {
        public ProductWithTypeAndBrand()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        public ProductWithTypeAndBrand(int id):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

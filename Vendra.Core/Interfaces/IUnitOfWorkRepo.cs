using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;

namespace Vendra.Core.Interfaces
{
    public interface IUnitOfWorkRepo
    {
        public IGenericRepo<Product> Products { get;}
        public IGenericRepo<ProductBrand> ProductBrands { get;  }
        public IGenericRepo<ProductType> ProductTypes { get;  }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        

    }
}

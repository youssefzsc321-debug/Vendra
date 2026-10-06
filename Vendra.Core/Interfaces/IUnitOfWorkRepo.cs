using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Interfaces
{
    public interface IUnitOfWorkRepo:IDisposable
    {
        public IGenericRepo<Product> Products { get;}
        public IGenericRepo<ProductBrand> ProductBrands { get;  }
        public IGenericRepo<ProductType> ProductTypes { get;  }
        public IGenericRepo<Order>Orders { get;  }
        public IGenericRepo<DeliveryMethod> DeliveryMethods { get;  }
        
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        

    }
}

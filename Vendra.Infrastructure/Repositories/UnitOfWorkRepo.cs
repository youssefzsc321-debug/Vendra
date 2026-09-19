using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Data;
using Vendra.Core.Enities;
using Vendra.Core.Interfaces;

namespace Vendra.Infrastructure.Repositories
{
    public class UnitOfWorkRepo : IUnitOfWorkRepo
    {
        private readonly VendraContext _context;
        public IGenericRepo<Product> Products { get; }

        public IGenericRepo<ProductBrand> ProductBrands { get; }

        public IGenericRepo<ProductType> ProductTypes { get; }

        public UnitOfWorkRepo(VendraContext context)
        {
            _context = context;
            Products=new GenericRepo<Product>(_context);
            ProductBrands=new GenericRepo<ProductBrand>(_context);
            ProductTypes = new GenericRepo<ProductType>(_context);
        }
        public int SaveChanges()
        {
          return _context.SaveChanges();    
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
